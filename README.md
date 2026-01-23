# MyPL - Custom Programming Language Compiler

A comprehensive compiler implementation for a custom programming language built with C# and ANTLR4. MyPL demonstrates lexical analysis, parsing, semantic analysis, and detailed compilation reporting.

## Features

### Language Support
- **Primitive Data Types**: `int`, `float`, `double`, `string`
- **Variables**: Mutable and constant (`const`) declarations
- **Functions**: User-defined functions with parameters and return types
- **Control Structures**: `if`/`else`, `for`, `while` loops
- **Operators**: 
  - Arithmetic: `+`, `-`, `*`, `/`, `%`
  - Comparison: `<`, `>`, `<=`, `>=`, `==`, `!=`
  - Logical: `&&`, `||`, `!`
  - Assignment: `=`, `+=`, `-=`, `*=`, `/=`, `%=`
  - Increment/Decrement: `++`, `--` (prefix and postfix)

### Compiler Capabilities
- **Lexical Analysis**: Tokenizes source code and identifies lexical errors
- **Syntax Analysis**: Parses token streams according to grammar rules
- **Semantic Analysis**: 
  - Type checking and type inference
  - Variable declaration and scope validation
  - Function signature validation
  - Constant assignment protection
  - Undefined variable/function detection
- **Comprehensive Reporting**: Generates detailed reports for:
  - All tokens extracted
  - Global variables
  - Function declarations and metadata
  - Compilation errors with line numbers

## Project Structure

```
MyPL/
├── MyPL.g4                     # ANTLR grammar definition
├── Program.cs                  # Entry point
├── MyPL.csproj                 # Project configuration
├── input.txt                   # Source code input file
├── Analysis/
│   └── SemanticAnalyzer.cs    # Semantic analysis and type checking
├── Core/
│   └── Compiler.cs            # Compilation orchestration
├── Domain/
│   └── Models.cs              # Data models and compilation result
├── Reporting/
│   └── ReportGenerator.cs     # Output report generation
├── Tests/
│   └── UnitTests.cs           # Unit tests for compiler validation
└── Output/                     # Generated reports
    ├── tokens.txt             # Token stream
    ├── global_variables.txt   # Global variable declarations
    ├── functions.txt          # Function metadata
    └── errors.txt             # Compilation errors
```

## Requirements

- **.NET 9.0 SDK** or higher
- **ANTLR4** Runtime (4.13.1)
- **ANTLR4 Build Tasks** (12.11.0)

## Installation

1. Clone the repository:
   ```bash
   git clone <repository-url>
   cd MyPL
   ```

2. Restore NuGet packages:
   ```bash
   dotnet restore
   ```

3. Build the project:
   ```bash
   dotnet build
   ```

## Usage

### Running the Compiler

Execute the compiler with a source file:

```bash
dotnet run [input-file]
```

If no input file is specified, the compiler uses `input.txt` in the project root.

### Example Program

Create a file with MyPL code:

```plaintext
int globalX = 10;
const int MAX_VALUE = 100;

int add(int a, int b) {
    return a + b;
}

void main() {
    int result = add(5, 3);
    if (result > 0) {
        result = result * 2;
    }
    
    for (int i = 0; i < 5; i = i + 1) {
        result += i;
    }
}
```

### Compilation Output

After compilation, check the `Output/` directory for:

- **tokens.txt**: Complete token stream from lexical analysis
- **global_variables.txt**: All global variable declarations with types and initial values
- **functions.txt**: Function signatures, parameters, local variables, and control flow information
- **errors.txt**: Syntax and semantic errors with line numbers

## Grammar Overview

### Program Structure
```antlr
program: (globalDeclaration | functionDeclaration)* EOF;
```

### Variable Declarations
```antlr
variableDeclaration:
    CONST? type ID (ASSIGN expression)? SEMICOLON
    | CONST? type ID ASSIGN expression (COMMA ID ASSIGN expression)* SEMICOLON;
```

### Function Declarations
```antlr
functionDeclaration:
    type ID LPAREN parameterList? RPAREN block
    | VOID ID LPAREN parameterList? RPAREN block;
```

## Semantic Analysis Features

The semantic analyzer performs comprehensive validation:

- **Type Safety**: Ensures type compatibility in assignments and operations
- **Scope Management**: Validates variable visibility (global vs. local scope)
- **Constant Protection**: Prevents reassignment to `const` variables
- **Function Validation**: 
  - Checks function signature uniqueness
  - Validates parameter types in function calls
  - Ensures return type compatibility
- **Expression Type Inference**: Deduces types for complex expressions
- **Control Flow Validation**: Validates control structures and return statements

## Error Reporting

MyPL provides detailed error messages with line numbers:

```
Line 3: Syntax Error: missing ';' at 'string'
Line 7: Undefined variable 'total' in local scope
Line 11: Undefined function 'statusText(string)'
Line 12: Invalid statement '@a'
```

## Development

### Modifying the Grammar

1. Edit `MyPL.g4` to change language syntax
2. Rebuild the project to regenerate parser/lexer classes:
   ```bash
   dotnet build
   ```
   ANTLR4 Build Tasks automatically generates the required C# classes.

### Extending Semantic Analysis

Add new validation rules in `Analysis/SemanticAnalyzer.cs` by:
- Overriding visitor methods for specific grammar rules
- Adding type checking logic
- Implementing scope resolution for new constructs

### Custom Report Generation

Modify `Reporting/ReportGenerator.cs` to customize output formats or add new reports.

## Testing

The project includes a suite of unit tests to validate compiler functionality.

### Running Tests

Tests are automatically executed when running the program. The test suite includes:

- **Test_ValidProgram**: Validates successful compilation of syntactically and semantically correct code
- **Test_RedeclarationError**: Ensures variable redeclaration is detected and reported
- **Test_TypeMismatch**: Verifies type checking catches incompatible assignments
- **Test_MissingMain**: Confirms the compiler requires a `main()` function entry point

### Adding New Tests

To add new test cases, edit `Tests/UnitTests.cs`:

```csharp
static bool Test_YourNewTest()
{
    var code = @"
        // Your test code here
    ";
    var result = new Compiler().Compile(code);
    return /* your assertion */;
}
```

Then register the test in the `Run()` method:

```csharp
total++;
if (Test_YourNewTest()) passed++;
else Console.WriteLine("FAIL: Test_YourNewTest");
```

## Architecture

### Compilation Pipeline

```
Source Code (input.txt)
    ↓
Lexical Analysis (ANTLR Lexer)
    ↓
Token Stream
    ↓
Syntax Analysis (ANTLR Parser)
    ↓
Parse Tree
    ↓
Semantic Analysis (Visitor Pattern)
    ↓
Symbol Tables & Type Information
    ↓
Report Generation
    ↓
Output Files (Output/ directory)
```

### Key Components

- **MyPLLexer**: Tokenizes input into language terminals
- **MyPLParser**: Builds parse tree from token stream
- **SemanticAnalyzer**: Walks parse tree, validates semantics, builds symbol tables
- **Compiler**: Orchestrates compilation phases and error collection
- **ReportGenerator**: Produces human-readable compilation reports

## Contributing

Contributions are welcome! Areas for enhancement:

- Additional data types (arrays, structs, booleans)
- More operators and expressions
- Enhanced error recovery
- Code optimization passes
- Intermediate representation (IR) generation
- Code generation to assembly or bytecode

## License

[Specify your license here]

## Acknowledgments

- Built with [ANTLR4](https://www.antlr.org/) - Parser generator for reading, processing, and executing structured text
- Developed with [.NET 9.0](https://dotnet.microsoft.com/)

---

**Author**: [Your Name]  
**Version**: 1.0.0  
**Last Updated**: January 2026
