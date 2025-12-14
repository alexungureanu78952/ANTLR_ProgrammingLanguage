using System.Collections.Generic;
using Antlr4.Runtime;
using MyPL.Analysis;
using MyPL.Domain;
using MyPL; // Required for Lexer/Parser
using System;

namespace MyPL.Core
{
    // Custom Error Listener to capture lexical and syntax errors
    public class CompilerErrorListener : BaseErrorListener
    {
        public List<string> Errors { get; } = new();

        public override void SyntaxError(
            System.IO.TextWriter output,
            IRecognizer recognizer,
            IToken offendingSymbol,
            int line,
            int charPositionInLine,
            string msg,
            RecognitionException e)
        {
            string errorMsg = $"Line {line}: Syntax Error: {msg}";
            Errors.Add(errorMsg);
            Console.WriteLine(errorMsg); // Output to console as well
        }
    }

    public class Compiler
    {
        public CompilationResult Compile(string sourceCode)
        {
            var allErrors = new List<string>();

            // 1. Lexical Analysis
            var inputStream = CharStreams.fromString(sourceCode);
            var lexer = new MyPLLexer(inputStream);

            // Add custom error listener to lexer
            var lexerErrorListener = new CompilerErrorListener();
            lexer.RemoveErrorListeners();
            lexer.AddErrorListener(lexerErrorListener);

            var tokenStream = new CommonTokenStream(lexer);
            tokenStream.Fill(); // Load all tokens

            // Extract Tokens for Report
            var tokenStrings = new List<string>();
            foreach (var t in tokenStream.GetTokens())
            {
                if (t.Type == -1) continue;
                string name = lexer.Vocabulary.GetSymbolicName(t.Type);
                tokenStrings.Add($"<{name}, {t.Text.Replace("\n", "\\n")}, {t.Line}>");
            }

            // Collect lexer errors
            allErrors.AddRange(lexerErrorListener.Errors);

            // 2. Parsing
            var parser = new MyPLParser(tokenStream);

            // Add custom error listener to parser
            var parserErrorListener = new CompilerErrorListener();
            parser.RemoveErrorListeners();
            parser.AddErrorListener(parserErrorListener);

            var tree = parser.program();

            // Collect parser errors
            allErrors.AddRange(parserErrorListener.Errors);

            // 3. Semantic Analysis
            var analyzer = new SemanticAnalyzer();
            analyzer.Visit(tree);

            // Collect semantic errors
            allErrors.AddRange(analyzer.Errors);

            // 4. Return Result
            return new CompilationResult(
                tokenStrings,
                analyzer.GlobalVariables,
                new List<FunctionInfo>(analyzer.Functions.Values),
                allErrors
            );
        }
    }
}