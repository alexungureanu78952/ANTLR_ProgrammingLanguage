using System;
using System.Collections.Generic;
using MyPL.Core; // Changed from MyPL to MyPL.Core
using MyPL.Domain;

namespace MyPL.Tests
{
    public class SimpleTestRunner
    {
        public static void Run()
        {
            Console.WriteLine("\n=== RUNNING UNIT TESTS ===\n");
            int passed = 0;
            int total = 0;

            // Test 1: Basic Valid Program
            total++;
            if (Test_ValidProgram()) passed++;
            else Console.WriteLine("FAIL: Test_ValidProgram");

            // Test 2: Redeclaration Error
            total++;
            if (Test_RedeclarationError()) passed++;
            else Console.WriteLine("FAIL: Test_RedeclarationError");

            // Test 3: Type Mismatch
            total++;
            if (Test_TypeMismatch()) passed++;
            else Console.WriteLine("FAIL: Test_TypeMismatch");

            // Test 4: Missing Main
            total++;
            if (Test_MissingMain()) passed++;
            else Console.WriteLine("FAIL: Test_MissingMain");

            Console.WriteLine($"\nResult: {passed}/{total} tests passed.");
            Console.WriteLine("==========================\n");
        }

        static bool Test_ValidProgram()
        {
            var code = @"
                int x = 10;
                void main() {
                    int y = x;
                }
            ";
            var result = new Compiler().Compile(code);
            return result.IsSuccess && result.GlobalVariables.Count == 1;
        }

        static bool Test_RedeclarationError()
        {
            var code = @"
                void main() {
                    int x = 10;
                    int x = 5; 
                }
            ";
            var result = new Compiler().Compile(code);
            return !result.IsSuccess && result.Errors[0].Contains("redeclared");
        }

        static bool Test_TypeMismatch()
        {
            var code = @"
                void main() {
                    int x = 5.5; 
                }
            ";
            var result = new Compiler().Compile(code);
            return !result.IsSuccess && result.Errors[0].Contains("Type Mismatch");
        }

        static bool Test_MissingMain()
        {
            var code = @"
                int x = 10;
            ";
            var result = new Compiler().Compile(code);
            return !result.IsSuccess && result.Errors[0].Contains("Missing 'main'");
        }
    }
}