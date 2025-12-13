using System;
using System.IO;
using MyPL.Reporting;
using MyPL.Core; // Added using

namespace MyPL
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0 && args[0] == "--test")
            {
                MyPL.Tests.SimpleTestRunner.Run();
                return;
            }

            string inputFile = args.Length > 0 ? args[0] : "input.txt";
            EnsureInputExists(inputFile);

            Console.WriteLine($"[Main] Reading {inputFile}...");
            string sourceCode = File.ReadAllText(inputFile);

            // Orchestration
            var compiler = new Compiler();
            var result = compiler.Compile(sourceCode);

            // Reporting
            var reporter = new ReportGenerator("Output");
            reporter.GenerateAll(result);

            if (result.IsSuccess)
                Console.WriteLine("[Main] Compilation Successful.");
            else
                Console.WriteLine($"[Main] Compilation Failed with {result.Errors.Count} errors.");
        }

        static void EnsureInputExists(string path)
        {
            if (!File.Exists(path))
            {
                File.WriteAllText(path, @"
int globalX = 10;
void main() {
    int localY = 5;
    if (localY > 0) {
        localY = localY - 1;
    }
}
");
                Console.WriteLine($"[Main] Created sample input: {path}");
            }
        }
    }
}
