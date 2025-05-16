using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers.Extensions.Symbols;
using static MVVMGenerators.Helpers.Descriptions.General;
using static MVVMGenerators.Helpers.Descriptions.Classes;

namespace MVVMGenerators.Generators.ViewModels.Data.Members;

public sealed class BindableCommand : BindableMember<IMethodSymbol>
{
    public readonly string CanExecute;

    public BindableCommand(IMethodSymbol command, string? canExecute, bool isLambda, bool isMethod)
        : base(command, BindMode.OneTime, GetTypeName(command), $"{command.GetFieldName()}Command", $"{command.GetPropertyName()}Command", "Command")
    {
        CanExecute = GetCanExecuteAction(command, isLambda, isMethod, canExecute);
    }

    public string ToDeclarationCommandString()
    {
        return
            $"""
            {GeneratedCodeViewModelAttribute}
            [{EditorBrowsableAttribute}({EditorBrowsableState}.Never)]
            private {Type} {SourceName};
            
            {GeneratedCodeViewModelAttribute}
            private {Type} {GeneratedName} => {SourceName} ??= new {Type}({Member.Name}{CanExecute});
            """;
    }
    
    private static string GetTypeName(IMethodSymbol command)
    {
        var type = new StringBuilder(RelayCommand);
        var parameters = command.Parameters;
        if (parameters.Length <= 0) return type.ToString();
        
        type.Append("<");

        foreach (var parameter in parameters)
            type.Append($"{parameter.Type.ToDisplayStringGlobal()},");

        type.Length--;
        type.Append(">");

        return type.ToString();
    }
    
    private static string GetCanExecuteAction(IMethodSymbol command, bool isLambda, bool isMethod, string? canExecute)
    {
        var canExecuteName = new StringBuilder(canExecute ?? "");
            
        if (canExecuteName.Length != 0)
        {
            if (!isLambda)
            {
                canExecuteName.Insert(0, ", ");
            }
            else
            {
                var parameters = command.Parameters;
                var missingParameters = string.Join(", ", Enumerable.Repeat("_", parameters.Length));
                
                canExecuteName.Insert(0, $", ({missingParameters}) => ");
                if (isLambda && isMethod) canExecuteName.Append("()");
            }
        }

        return canExecuteName.ToString();
    }
}