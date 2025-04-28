using System.Text;
using Microsoft.CodeAnalysis;
using MVVMGenerators.Generators.Ids;
using MVVMGenerators.Helpers.Descriptions;
using MVVMGenerators.Helpers.Extensions.Symbols;

namespace MVVMGenerators.Generators.ViewModels.Data.Members;

public readonly struct RelayCommandData(
    IMethodSymbol execute, 
    string? canExecuteName = null,
    bool isMethod = false,
    bool isLambda = false)
{
    public readonly IMethodSymbol Execute = execute;

    public readonly string PropertyName = $"{execute.Name}Command";
    public readonly string Id = $"{Classes.Ids.Global}.{execute.GetId("Command")}";
    public readonly string FieldName = $"_{PropertySymbolExtensions.GetFieldName(execute.Name)}Command";
    
    public readonly bool IsLambda = isLambda;
    public readonly bool IsMethod = isMethod;
    public readonly string? CanExecuteName = canExecuteName;

    public string GetTypeName()
    {
        var type = new StringBuilder(Classes.RelayCommand.Global);
        var parameters = Execute.Parameters;
        if (parameters.Length <= 0) return type.ToString();
        
        type.Append("<");

        foreach (var parameter in parameters)
            type.Append($"{parameter.Type.ToDisplayStringGlobal()},");

        type.Length--;
        type.Append(">");

        return type.ToString();
    }
}