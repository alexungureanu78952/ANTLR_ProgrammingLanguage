using System.Collections.Generic;

namespace MyPL.Domain
{
    public record VariableInfo(string Name, string Type, string InitValue, bool IsConst, int Line, string Scope);

    public class FunctionInfo
    {
        public string Name { get; set; } = string.Empty;
        public string ReturnType { get; set; } = "void";
        public bool IsMain { get; set; }
        public bool IsRecursive { get; set; }
        public int Line { get; set; }
        
        public List<VariableInfo> Parameters { get; } = new();
        public List<VariableInfo> Locals { get; } = new();
        public List<string> ControlStructures { get; } = new();
    }

    public record CompilationResult(
        List<string> Tokens,
        List<VariableInfo> GlobalVariables,
        List<FunctionInfo> Functions,
        List<string> Errors
    )
    {
        public bool IsSuccess => Errors.Count == 0;
    }
}
