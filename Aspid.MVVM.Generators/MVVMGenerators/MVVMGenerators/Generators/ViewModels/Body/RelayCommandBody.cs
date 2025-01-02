using System.Linq;
using System.Text;
using MVVMGenerators.Helpers;
using MVVMGenerators.Helpers.Descriptions;
using MVVMGenerators.Helpers.Extensions.Writer;
using MVVMGenerators.Generators.ViewModels.Data;
using MVVMGenerators.Generators.ViewModels.Data.Members;

namespace MVVMGenerators.Generators.ViewModels.Body;

public static class RelayCommandBody
{
    private const string GeneratedAttribute = General.GeneratedCodeViewModelAttribute;
    
    private static readonly string EditorBrowsableAttribute = $"[{Classes.EditorBrowsableAttribute.Global}({Classes.EditorBrowsableState.Global}.Never)]";
    
    public static CodeWriter AppendRelayCommandBody(this CodeWriter code, in ViewModelDataSpan data)
    {
        var index = 0;
        var count = data.Commands.Length;
        
        foreach (var command in data.Commands)
        {
            var fieldName = command.FieldName;
            var methodName = command.Execute.Name;
            var propertyName = command.PropertyName;
            
            var type = command.GetTypeName();
            var canExecuteAction = GetCanExecuteAction(command);
            
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

    private static string GetCanExecuteAction(in RelayCommandData command)
    {
        var canExecuteName = new StringBuilder(command.CanExecuteName ?? "");
            
        if (canExecuteName.Length != 0)
        {
            if (!command.IsLambda)
            {
                canExecuteName.Insert(0, ", ");
            }
            else
            {
                var parameters = command.Execute.Parameters;
                var missingParameters = string.Join(", ", Enumerable.Repeat("_", parameters.Length));
                
                canExecuteName.Insert(0, $", ({missingParameters}) => ");
                if (command.IsMethod) canExecuteName.Append("()");
            }
        }

        return canExecuteName.ToString();
    }
}