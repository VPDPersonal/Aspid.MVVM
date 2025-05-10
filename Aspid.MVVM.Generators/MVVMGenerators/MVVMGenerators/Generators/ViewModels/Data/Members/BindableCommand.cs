using System.Text;
using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers.Descriptions;
using MVVMGenerators.Helpers.Extensions.Symbols;

namespace MVVMGenerators.Generators.ViewModels.Data.Members;

public sealed class BindableCommand(IMethodSymbol command, string? canExecute, bool isLambda)
    : BindableMember(command,
        BindMode.OneTime,
        GetTypeName(command),
        $"_{command.GetFieldName()}Command",
        $"{command.GetPropertyName()}Command",
        "Command")
{
    public readonly bool IsLambda = isLambda;
    public readonly string? CanExecute = canExecute;
    
    public override bool IsValueType => false;
    
    private static string GetTypeName(IMethodSymbol command)
    {
        var type = new StringBuilder(Classes.RelayCommand.Global);
        var parameters = command.Parameters;
        if (parameters.Length <= 0) return type.ToString();
        
        type.Append("<");

        foreach (var parameter in parameters)
            type.Append($"{parameter.Type.ToDisplayStringGlobal()},");

        type.Length--;
        type.Append(">");

        return type.ToString();
    }
}