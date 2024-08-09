using System;

namespace MVVMGenerators.Helpers.Extensions.Writer;

public static class CodeWriterIfExtensions
{
    public static CodeWriter AppendIf(this CodeWriter code, Func<bool> condition, string value = "") =>
        code.AppendIf(condition.Invoke(), value);

    public static CodeWriter AppendIf(this CodeWriter code, bool flag, string value = "") =>
        !flag ? code : code.Append(value);
    
    public static CodeWriter AppendLineIf(this CodeWriter code, Func<bool> condition, string value = "") =>
        code.AppendLineIf(condition.Invoke(), value);
    
    public static CodeWriter AppendLineIf(this CodeWriter code, bool flag, string value = "") =>
        !flag ? code : code.AppendLine(value);
    
    public static CodeWriter AppendMultilineIf(this CodeWriter code, Func<bool> condition, string value = "") =>
        code.AppendMultilineIf(condition.Invoke(), value);
    
    public static CodeWriter AppendMultilineIf(this CodeWriter code, bool flag, string value = "") =>
        !flag ? code : code.AppendMultiline(value);
    
    public static CodeWriter AppendChildIf(this CodeWriter code, Func<bool> condition, Func<CodeWriter> childFunctions) =>
        code.AppendChildIf(condition.Invoke(), childFunctions);
    
    public static CodeWriter AppendChildIf(this CodeWriter code, bool flag, Func<CodeWriter> childFunctions) =>
        !flag ? code : childFunctions.Invoke();

    public static CodeWriter BeginBlockIf(this CodeWriter code, Func<bool> condition) =>
        code.BeginBlockIf(condition.Invoke());
    
    public static CodeWriter BeginBlockIf(this CodeWriter code, bool flag) =>
        !flag ? code : code.BeginBlock();
    
    public static CodeWriter EndBlockIf(this CodeWriter code, Func<bool> condition) =>
        code.EndBlockIf(condition.Invoke());
    
    public static CodeWriter EndBlockIf(this CodeWriter code, bool flag) =>
        !flag ? code : code.EndBlock();
}