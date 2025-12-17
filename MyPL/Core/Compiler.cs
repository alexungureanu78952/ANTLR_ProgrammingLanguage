using System.Collections.Generic;
using Antlr4.Runtime;
using MyPL.Analysis;
using MyPL.Domain;
using MyPL;
using System;

namespace MyPL.Core
{
    public class LexerErrorListener : IAntlrErrorListener<int>
    {
        public List<string> Errors { get; } = new();

        public void SyntaxError(
            System.IO.TextWriter output,
            IRecognizer recognizer,
            int offendingSymbol,
            int line,
            int charPositionInLine,
            string msg,
            RecognitionException e)
        {
            string errorMsg = $"Line {line}: Lexical Error: {msg}";
            Errors.Add(errorMsg);
            Console.WriteLine(errorMsg);
        }
    }

    public class ParserErrorListener : IAntlrErrorListener<IToken>
    {
        public List<string> Errors { get; } = new();

        public void SyntaxError(
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
            Console.WriteLine(errorMsg);
        }
    }

    public class Compiler
    {
        public CompilationResult Compile(string sourceCode)
        {
            var allErrors = new List<string>();

            var inputStream = CharStreams.fromString(sourceCode);
            var lexer = new MyPLLexer(inputStream);

            var lexerErrorListener = new LexerErrorListener();
            lexer.RemoveErrorListeners();
            lexer.AddErrorListener(lexerErrorListener);

            var tokenStream = new CommonTokenStream(lexer);
            tokenStream.Fill();

            var tokenStrings = new List<string>();
            foreach (var t in tokenStream.GetTokens())
            {
                if (t.Type == -1) continue;
                string name = lexer.Vocabulary.GetSymbolicName(t.Type);
                tokenStrings.Add($"<{name}, {t.Text.Replace("\n", "\\n")}, {t.Line}>");
            }

            allErrors.AddRange(lexerErrorListener.Errors);

            var parser = new MyPLParser(tokenStream);

            var parserErrorListener = new ParserErrorListener();
            parser.RemoveErrorListeners();
            parser.AddErrorListener(parserErrorListener);

            var tree = parser.program();

            allErrors.AddRange(parserErrorListener.Errors);

            var analyzer = new SemanticAnalyzer();
            analyzer.Visit(tree);

            allErrors.AddRange(analyzer.Errors);

            return new CompilationResult(
                tokenStrings,
                analyzer.GlobalVariables,
                new List<FunctionInfo>(analyzer.Functions.Values),
                allErrors
            );
        }
    }
}