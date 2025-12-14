using System;
using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime.Misc;
using MyPL.Domain;
using MyPL; // Required for MyPLParser

namespace MyPL.Analysis
{
    /// <summary>
    /// Visits the Parse Tree to extract semantic information and validate rules.
    /// Implements the core logic of the compiler.
    /// </summary>
    public class SemanticAnalyzer : MyPLBaseVisitor<object>
    {
        // Public Output Data
        public List<VariableInfo> GlobalVariables { get; } = new();
        public Dictionary<string, FunctionInfo> Functions { get; } = new();
        public List<string> Errors { get; } = new();

        // Internal State
        private string _currentScopeName = null;
        private FunctionInfo _currentFunction = null;
        private readonly Dictionary<string, VariableInfo> _symbolTable = new();

        // --- Error Helper ---
        private void ReportError(int line, string message)
        {
            string errorMsg = $"Line {line}: {message}";
            Errors.Add(errorMsg);
            Console.WriteLine(errorMsg); // Output to console as well
        }

        // --- Function Signature Helper ---
        private string GetFunctionSignature(string name, List<VariableInfo> parameters)
        {
            var paramTypes = string.Join(",", parameters.Select(p => p.Type));
            return $"{name}({paramTypes})";
        }

        // --- Symbol Resolution ---
        private VariableInfo ResolveVariable(string name)
        {
            if (_symbolTable.TryGetValue(name, out var sym)) return sym;
            // Fallback to globals if not found in local scope (redundant if loaded, but safe)
            return GlobalVariables.FirstOrDefault(v => v.Name == name);
        }

        // --- Type Checking Logic ---
        private static bool AreTypesCompatible(string targetType, string sourceType)
        {
            if (targetType == sourceType) return true;
            // Implicit casting rules
            if (targetType == "double" && (sourceType == "int" || sourceType == "float")) return true;
            if (targetType == "float" && sourceType == "int") return true;
            return false;
        }

        private string InferExpressionType(MyPLParser.ExpressionContext context)
        {
            if (context == null) return "void";

            if (context is MyPLParser.LiteralExprContext lit)
            {
                if (lit.literal().INT_LITERAL() != null) return "int";
                if (lit.literal().FLOAT_LITERAL() != null) return "float";
                if (lit.literal().STRING_LITERAL() != null) return "string";
            }
            else if (context is MyPLParser.IdentifierExprContext idExpr)
            {
                var sym = ResolveVariable(idExpr.ID().GetText());
                return sym?.Type ?? "unknown";
            }
            else if (context is MyPLParser.FunctionCallExprContext funcExpr)
            {
                string funcName = funcExpr.ID().GetText();
                var func = Functions.Values.FirstOrDefault(f => f.Name == funcName);
                return func?.ReturnType ?? "unknown";
            }

            // Simplification: Arithmetic operations generally result in int (or promoted type in a real compiler)
            return "int";
        }

        private bool HasReturnPath(MyPLParser.BlockContext block)
        {
            if (block.statement() == null) return false;
            foreach (var stmt in block.statement())
            {
                if (stmt.returnStatement() != null) return true;
                if (stmt.block() != null && HasReturnPath(stmt.block())) return true;
            }
            return false;
        }

        // --- Visitor Implementations ---

        public override object VisitProgram([NotNull] MyPLParser.ProgramContext context)
        {
            base.VisitProgram(context);

            // Global Semantic Checks
            var mainFuncs = Functions.Values.Where(f => f.IsMain).ToList();
            if (mainFuncs.Count == 0) ReportError(0, "Semantic Error: Missing 'main' function.");
            else if (mainFuncs.Count > 1) ReportError(0, "Semantic Error: Multiple 'main' functions defined.");

            return null;
        }

        public override object VisitVariableDeclaration([NotNull] MyPLParser.VariableDeclarationContext context)
        {
            // Add null checks to handle malformed parse trees from syntax errors
            if (context.type() == null || context.ID() == null || context.ID().Length == 0)
            {
                // Skip processing if parse tree is incomplete due to syntax errors
                return null;
            }

            string type = context.type().GetText();
            bool isConst = context.CONST() != null;
            var ids = context.ID();
            var exprs = context.expression();
            int exprIndex = 0;

            for (int i = 0; i < ids.Length; i++)
            {
                // Add null check for individual ID tokens
                if (ids[i] == null) continue;

                string name = ids[i].GetText();
                int line = ids[i].Symbol.Line;
                string initVal = "null";
                string initType = null;
                bool hasInit = false;

                // Handle 'int x=1, y=2' vs 'int x=1'
                if ((context.COMMA().Length > 0 && exprIndex < exprs.Length) || (exprs.Length > 0 && i == 0))
                {
                    if (exprIndex < exprs.Length && exprs[exprIndex] != null)
                    {
                        var expr = exprs[exprIndex++];
                        initType = InferExpressionType(expr);
                        initVal = expr.GetText();
                        hasInit = true;
                    }
                }

                var variable = new VariableInfo(name, type, initVal, isConst, line, _currentScopeName ?? "global");

                // Check Redeclaration
                if (_currentScopeName == null) // Global
                {
                    if (GlobalVariables.Any(v => v.Name == name))
                        ReportError(line, $"Global variable '{name}' redeclared.");
                    else
                    {
                        GlobalVariables.Add(variable);
                        _symbolTable[name] = variable;
                    }
                }
                else // Local
                {
                    if (_currentFunction.Locals.Any(v => v.Name == name) || _currentFunction.Parameters.Any(p => p.Name == name))
                        ReportError(line, $"Local variable '{name}' redeclared in function '{_currentScopeName}'.");
                    else
                    {
                        _currentFunction.Locals.Add(variable);
                        _symbolTable[name] = variable;
                    }
                }

                // Check Type Compatibility
                if (hasInit && initType != "unknown" && !AreTypesCompatible(type, initType))
                {
                    ReportError(line, $"Type Mismatch: Cannot assign '{initType}' to '{type}' for variable '{name}'.");
                }
            }
            return base.VisitVariableDeclaration(context);
        }

        public override object VisitFunctionDeclaration([NotNull] MyPLParser.FunctionDeclarationContext context)
        {
            string name = context.ID().GetText();

            var func = new FunctionInfo
            {
                Name = name,
                ReturnType = context.type()?.GetText() ?? "void",
                Line = context.Start.Line,
                IsMain = (name == "main")
            };

            // --- Enter Scope ---
            _currentScopeName = name;
            _currentFunction = func;

            // Snapshot global scope to restore later
            var scopeSnapshot = new Dictionary<string, VariableInfo>(_symbolTable);
            _symbolTable.Clear();
            foreach (var g in GlobalVariables) _symbolTable[g.Name] = g;

            // Process Parameters
            if (context.parameterList() != null)
            {
                foreach (var pCtx in context.parameterList().parameter())
                {
                    string pName = pCtx.ID().GetText();
                    var pSym = new VariableInfo(pName, pCtx.type().GetText(), "arg", false, pCtx.Start.Line, name);

                    if (func.Parameters.Any(p => p.Name == pName))
                        ReportError(pCtx.Start.Line, $"Parameter '{pName}' duplicated in function '{name}'.");
                    else
                    {
                        func.Parameters.Add(pSym);
                        _symbolTable[pName] = pSym;
                    }
                }
            }

            // Check for duplicate function signature (same name AND same parameter types)
            string signature = GetFunctionSignature(name, func.Parameters);
            if (Functions.ContainsKey(signature))
            {
                ReportError(context.Start.Line, $"Function '{name}' with same parameter types is already defined.");
                return null;
            }
            Functions[signature] = func;

            Visit(context.block()); // Visit body

            // Check Return Requirement
            if (func.ReturnType != "void" && !func.IsMain && !HasReturnPath(context.block()))
            {
                ReportError(context.Stop.Line, $"Function '{name}' must return a value of type '{func.ReturnType}'.");
            }

            // --- Exit Scope ---
            _currentScopeName = null;
            _currentFunction = null;
            _symbolTable.Clear();
            foreach (var kvp in scopeSnapshot) _symbolTable[kvp.Key] = kvp.Value;

            return null;
        }

        public override object VisitFunctionCallExpr([NotNull] MyPLParser.FunctionCallExprContext context)
        {
            string name = context.ID().GetText();

            // Recursivity Checks
            if (name == "main") ReportError(context.Start.Line, "Error: Cannot call 'main' function.");
            if (name == _currentScopeName && _currentFunction != null) _currentFunction.IsRecursive = true;

            // Get argument types to find matching overload
            var argExprs = context.argumentList()?.expression();
            int argCount = argExprs?.Length ?? 0;

            // Find all functions with matching name
            var matchingFuncs = Functions.Values.Where(f => f.Name == name).ToList();

            if (matchingFuncs.Count == 0)
            {
                ReportError(context.Start.Line, $"Error: Call to undefined function '{name}'.");
            }
            else
            {
                // Try to find exact match by parameter count
                var exactMatch = matchingFuncs.FirstOrDefault(f => f.Parameters.Count == argCount);

                if (exactMatch == null)
                {
                    var availableCounts = string.Join(", ", matchingFuncs.Select(f => f.Parameters.Count).Distinct());
                    ReportError(context.Start.Line, $"Error: No overload of '{name}' takes {argCount} arguments. Available: {availableCounts} parameters.");
                }
                else
                {
                    // Check argument types compatibility
                    if (argExprs != null)
                    {
                        for (int i = 0; i < argCount; i++)
                        {
                            string argType = InferExpressionType(argExprs[i]);
                            string paramType = exactMatch.Parameters[i].Type;

                            if (argType != "unknown" && !AreTypesCompatible(paramType, argType))
                            {
                                ReportError(context.Start.Line,
                                    $"Error: Argument {i + 1} type mismatch in call to '{name}'. Expected '{paramType}', got '{argType}'.");
                            }
                        }
                    }

                    if (name == _currentScopeName && _currentFunction != null)
                    {
                        _currentFunction.IsRecursive = true;
                    }
                }
            }

            return base.VisitFunctionCallExpr(context);
        }

        public override object VisitIdentifierExpr([NotNull] MyPLParser.IdentifierExprContext context)
        {
            string name = context.ID().GetText();
            if (ResolveVariable(name) == null)
            {
                ReportError(context.Start.Line, $"Error: Variable '{name}' is not declared.");
            }
            return base.VisitIdentifierExpr(context);
        }

        public override object VisitAssignmentStatement([NotNull] MyPLParser.AssignmentStatementContext context)
        {
            string name = context.ID().GetText();
            var sym = ResolveVariable(name);

            if (sym == null)
            {
                ReportError(context.Start.Line, $"Error: Variable '{name}' is not declared.");
            }
            else
            {
                if (sym.IsConst) ReportError(context.Start.Line, $"Error: Cannot assign to constant '{name}'.");

                // Type check assignment
                /* (Simplified logic, assumes expression visit handles errors) */
            }
            return base.VisitAssignmentStatement(context);
        }

        // Control Structures Tracking
        public override object VisitIfStatement([NotNull] MyPLParser.IfStatementContext context)
        {
            _currentFunction?.ControlStructures.Add($"if (Line {context.Start.Line})"); return base.VisitIfStatement(context);
        }
        public override object VisitWhileStatement([NotNull] MyPLParser.WhileStatementContext context)
        {
            _currentFunction?.ControlStructures.Add($"while (Line {context.Start.Line})"); return base.VisitWhileStatement(context);
        }
        public override object VisitForStatement([NotNull] MyPLParser.ForStatementContext context)
        {
            _currentFunction?.ControlStructures.Add($"for (Line {context.Start.Line})"); return base.VisitForStatement(context);
        }
    }
}
