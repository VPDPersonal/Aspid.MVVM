using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers;
using MVVMGenerators.Helpers.Data;
using MVVMGenerators.Helpers.Descriptions;
using MVVMGenerators.Helpers.Extensions.Writer;
using MVVMGenerators.Generators.ViewModels.Data;
using MVVMGenerators.Generators.ViewModels.Data.Members;

namespace MVVMGenerators.Generators.ViewModels.Body;

public static class RelayCommandBody
{
    private const string GeneratedAttribute = General.GeneratedCodeViewModelAttribute;
    private static readonly string EditorBrowsableAttribute = $"[{Classes.EditorBrowsableAttribute.Global}({Classes.EditorBrowsableState.Global}.Never)]";

    public static void Generate(
        string @namespace,
        in ViewModelDataSpan data,
        in DeclarationText declaration,
        in SourceProductionContext context)
    {
        if (data.MembersByType.Commands.IsEmpty) return;
        
        var code = new CodeWriter();
            
        code.AppendClassBegin(@namespace, declaration)
            .AppendRelayCommandBody(data.MembersByType.Commands)
            .AppendClassEnd(@namespace);
            
        context.AddSource(declaration.GetFileName(@namespace, "Commands"), code.GetSourceText());
    }
    
    private static CodeWriter AppendRelayCommandBody(this CodeWriter code, in CastedSpan<BindableMember, BindableCommand> bindableCommands)
    {
        var index = 0;
        var count = bindableCommands.Length;
        
        foreach (var command in bindableCommands)
        {
            var fieldName = command.SourceName;
            var methodName = command.Member.Name;
            var propertyName = command.GeneratedName;
            
            var type = command.Type;
            var canExecuteAction = GetCanExecuteAction((BindableCommand)command);
            
            code.AppendMultiline(
                $"""
                {EditorBrowsableAttribute}
                {GeneratedAttribute}
                private {type} {fieldName};
                
                {GeneratedAttribute}
                private {type} {propertyName} => {fieldName} ??= new {type}({methodName}{canExecuteAction});
                """);

            index++;
            code.AppendLineIf(index < count);
        }

        return code;
    }

    private static string GetCanExecuteAction(BindableCommand command)
    {
        var canExecuteName = new StringBuilder(command.CanExecute ?? "");
            
        if (canExecuteName.Length != 0)
        {
            if (!command.IsLambda)
            {
                canExecuteName.Insert(0, ", ");
            }
            else
            {
                var parameters = ((IMethodSymbol)command.Member).Parameters;
                var missingParameters = string.Join(", ", Enumerable.Repeat("_", parameters.Length));
                
                canExecuteName.Insert(0, $", ({missingParameters}) => ");
                if (command.IsLambda) canExecuteName.Append("()");
            }
        }

        return canExecuteName.ToString();
    }
}