using System.Linq;
using System.Text;
using Aspid.Generators.Helper;
using Aspid.MVVM.Generators.Generators.Ids.Data;
using Aspid.MVVM.Generators.Helpers;
using Microsoft.CodeAnalysis;
using static Aspid.Generators.Helper.Classes;
using static Aspid.MVVM.Generators.Generators.Descriptions.Classes;
using static Aspid.MVVM.Generators.Generators.Descriptions.General;

namespace Aspid.MVVM.Generators.Generators.ViewModels.Data.Infos;

public sealed class BindableCommandInfo : IBindableMemberInfo
{
    public string Type { get; }
    
    public string Name { get; }
    
    public IdData Id { get; }

    public BindMode Mode => BindMode.OneTime;
    
    public GeneratedBindableMembers Bindable { get; }
    
    public string CommandDeclaration { get; }

    public BindableCommandInfo(IMethodSymbol methodSymbol, string? canExecute, bool isLambda, bool isMethod)
    {
        Type = GetTypeName(methodSymbol);
        Id = new IdData(methodSymbol, "Command");
        Name = $"{methodSymbol.GetPropertyName()}Command";
        Bindable = GeneratedBindableMembers.CreateForRelayCommand(Type, Name);

        var fieldName = $"{methodSymbol.GetFieldName("__")}Command";
        canExecute = GetCanExecuteAction(methodSymbol, isLambda, isMethod, canExecute);
        
        CommandDeclaration = 
            $"""
            {GeneratedCodeViewModelAttribute}
            [{EditorBrowsableAttribute}({EditorBrowsableState}.Never)]
            private {Type} {fieldName};

            {GeneratedCodeViewModelAttribute}
            private {Type} {Name} => {fieldName} ??= new {Type}({methodSymbol.Name}{canExecute});
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
