using System.Collections.Generic;
using Antlr4.Runtime;
using MyPL.Analysis;
using MyPL.Domain;
using MyPL; // Required for Lexer/Parser

namespace MyPL.Core
{
    public class Compiler
    {
        public CompilationResult Compile(string sourceCode)
        {
            // 1. Lexical Analysis
            var inputStream = CharStreams.fromString(sourceCode);
            var lexer = new MyPLLexer(inputStream);
            var tokenStream = new CommonTokenStream(lexer);
            tokenStream.Fill(); // Load all tokens

            // Extract Tokens for Report
            var tokenStrings = new List<string>();
            foreach (var t in tokenStream.GetTokens())
            {
                if (t.Type == -1) continue;
                string name = lexer.Vocabulary.GetSymbolicName(t.Type);
                tokenStrings.Add($"<{name}, {t.Text.Replace("\n","\\n")}, {t.Line}>");
            }

            // 2. Parsing
            var parser = new MyPLParser(tokenStream);
            var tree = parser.program();

            // 3. Semantic Analysis
            var analyzer = new SemanticAnalyzer();
            analyzer.Visit(tree);

            // 4. Return Result
            return new CompilationResult(
                tokenStrings,
                analyzer.GlobalVariables,
                new List<FunctionInfo>(analyzer.Functions.Values),
                analyzer.Errors
            );
        }
    }
}