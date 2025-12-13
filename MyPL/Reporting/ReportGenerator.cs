using System;
using System.Collections.Generic;
using System.IO;
using Antlr4.Runtime;
using MyPL.Domain;

namespace MyPL.Reporting
{
    public class ReportGenerator
    {
        private readonly string _outputDir;

        public ReportGenerator(string outputDir)
        {
            _outputDir = outputDir;
            if (!Directory.Exists(_outputDir)) Directory.CreateDirectory(_outputDir);
        }

        public void GenerateAll(CompilationResult result)
        {
            WriteTokens(result.Tokens);
            WriteGlobals(result.GlobalVariables);
            WriteFunctions(result.Functions);
            WriteErrors(result.Errors);
            Console.WriteLine($"[Report] All reports generated in: {_outputDir}");
        }

        private void WriteTokens(IEnumerable<string> tokens)
        {
            File.WriteAllLines(Path.Combine(_outputDir, "tokens.txt"), tokens);
        }

        private void WriteGlobals(IEnumerable<VariableInfo> globals)
        {
            using var writer = new StreamWriter(Path.Combine(_outputDir, "global_variables.txt"));
            foreach (var v in globals)
            {
                writer.WriteLine($"Type: {v.Type}, Name: {v.Name}, Init: {v.InitValue}, Const: {v.IsConst}, Line: {v.Line}");
            }
        }

        private void WriteFunctions(IEnumerable<FunctionInfo> functions)
        {
            using var writer = new StreamWriter(Path.Combine(_outputDir, "functions.txt"));
            foreach (var f in functions)
            {
                string recursionType = f.IsRecursive ? "recursiva" : "iterativa";
                string mainType = f.IsMain ? "main" : "non-main";

                writer.WriteLine($"Function: {f.Name}");
                writer.WriteLine($"  Type: {mainType}, {recursionType}");
                writer.WriteLine($"  Return Type: {f.ReturnType}");
                
                writer.Write("  Parameters: ");
                if (f.Parameters.Count == 0) writer.WriteLine("None");
                else
                {
                    writer.WriteLine();
                    foreach (var p in f.Parameters) writer.WriteLine($"    {p.Type} {p.Name}");
                }

                writer.Write("  Local Variables: ");
                if (f.Locals.Count == 0) writer.WriteLine("None");
                else
                {
                    writer.WriteLine();
                    foreach (var l in f.Locals) writer.WriteLine($"    {l.Type} {l.Name} (Init: {l.InitValue})");
                }

                writer.Write("  Control Structures: ");
                if (f.ControlStructures.Count == 0) writer.WriteLine("None");
                else
                {
                    writer.WriteLine();
                    foreach (var s in f.ControlStructures) writer.WriteLine($"    {s}");
                }
                writer.WriteLine(new string('-', 30));
            }
        }

        private void WriteErrors(List<string> errors)
        {
            using var writer = new StreamWriter(Path.Combine(_outputDir, "errors.txt"));
            if (errors.Count == 0) writer.WriteLine("No errors found.");
            else foreach (var err in errors) writer.WriteLine(err);
        }
    }
}
