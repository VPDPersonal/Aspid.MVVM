using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Aspid.Generators.Helper;
using Aspid.MVVM.Generators.Helpers;
using Aspid.MVVM.Generators.Generators.Ids.Data;
using static Aspid.MVVM.Generators.Generators.Descriptions.Classes;
using static Aspid.MVVM.Generators.Generators.Descriptions.Constants;

namespace Aspid.MVVM.Generators.Generators.ViewModels.Data.Infos;

public sealed class BindableCommandInfo : IBindableMemberInfo
{
    public ISymbol Member { get; }
    
    public string Type { get; }
    
    public string Name { get; }
    
    public string CanExecute { get; }
    
    public IdData Id { get; }

    public BindMode Mode => BindMode.OneTime;
    
    public GeneratedBindableMembers Bindable { get; }
    
    public string CommandDeclaration { get; }

    public BindableCommandInfo(IMethodSymbol methodSymbol, string? canExecute, bool isLambda, bool isMethod)
    {
        Member = methodSymbol;
        Type = GetTypeName(methodSymbol);
        Id = new IdData(methodSymbol, "Command");
        Name = $"{methodSymbol.GetPropertyName()}Command";
        Bindable = GeneratedBindableMembers.CreateForRelayCommand(Type, Name);

        var fieldName = $"{methodSymbol.GetFieldName("__")}Command";
        CanExecute = GetCanExecuteAction(methodSymbol, isLambda, isMethod, canExecute);
        
        CommandDeclaration = 
            $"""
            #region {Name}
            {EditorBrowsableAttributeNever}
            {GeneratedCodeViewModelAttribute}
            private {Type} {fieldName};

            {GeneratedCodeViewModelAttribute}
            private {Type} {Name} => {fieldName} ??= new {Type}({methodSymbol.Name}{CanExecute});
            #endregion
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
        if (canExecute is null || string.IsNullOrWhiteSpace(canExecute)) return string.Empty;

        var canExecuteName = new StringBuilder(canExecute);
            
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

        return canExecuteName.ToString();
    }
}