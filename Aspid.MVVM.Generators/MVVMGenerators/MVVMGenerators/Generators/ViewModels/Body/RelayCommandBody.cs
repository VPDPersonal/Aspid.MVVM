using System.Linq;
using System.Text;
using MVVMGenerators.Helpers;
using MVVMGenerators.Helpers.Descriptions;
using MVVMGenerators.Helpers.Extensions.Writer;
using MVVMGenerators.Generators.ViewModels.Data;
using MVVMGenerators.Helpers.Extensions.Symbols;

namespace MVVMGenerators.Generators.ViewModels.Body;

public static class RelayCommandBody
{
    private const string GeneratedAttribute = General.GeneratedCodeViewModelAttribute;
    
    public static CodeWriter AppendRelayCommandBody(this CodeWriter code, in ViewModelDataSpan data)
    {
        var index = 0;
        var count = data.Commands.Length;
        
        foreach (var command in data.Commands)
        {
            var methodName = command.Execute.Name;
            var propertyName = $"{methodName}Command";
            var fieldName = PropertySymbolExtensions.GetFieldName(propertyName);
            
            var type = GetCommandType(command);
            var canExecuteName = GetCanExecuteName(command);
            
            code.AppendMultiline(
                $"""
                {GeneratedAttribute}
                private {type} {fieldName};
                
                {GeneratedAttribute}
                private {type} {propertyName} => {fieldName} ??= new {type}({methodName}{canExecuteName});
                """);

            index++;
            code.AppendLineIf(index < count);
        }

        return code;
    }

    private static string GetCommandType(in RelayCommandData command)
    {
        var type = new StringBuilder(Classes.RelayCommand.Global);
        var parameters = command.Execute.Parameters;
        if (parameters.Length <= 0) return type.ToString();
        
        type.Append("<");

        foreach (var parameter in parameters)
            type.Append($"{parameter.Type},");

        type.Length--;
        type.Append(">");

        return type.ToString();
    }

    private static string GetCanExecuteName(in RelayCommandData command)
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