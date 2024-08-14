using System.Linq;
using System.Text;
using MVVMGenerators.Helpers;
using MVVMGenerators.Helpers.Descriptions;
using MVVMGenerators.Helpers.Extensions.Symbols;

namespace MVVMGenerators.Generators.ViewModels.Body;

public static class RelayCommandBody
{
    private const string GeneratedAttribute = General.GeneratedCodeViewModelAttribute;
    
    public static CodeWriter AppendRelayCommandBody(this CodeWriter code, in ViewModelDataSpan data)
    {
        foreach (var command in data.Commands)
        {
            var methodName = command.Execute.Name;
            var propertyName = $"{methodName}Command";
            var fieldName = PropertySymbolExtensions.GetFieldName(propertyName);

            var type = new StringBuilder(Classes.RelayCommand.Global);
            var parameters = command.Execute.Parameters;

            if (parameters.Length > 0)
            {
                type.Append("<");

                foreach (var parameter in parameters)
                    type.Append($"{parameter.Type},");

                type.Length--;
                type.Append(">");
            }

            var canExecuteName = command.CanExecuteName ?? "";
            
            if (!string.IsNullOrEmpty(canExecuteName))
            {
                if (!command.IsLambda)
                {
                    canExecuteName = $", {canExecuteName}";
                }
                else
                {
                    canExecuteName = command.IsMethod 
                        ? $", ({string.Join(", ", Enumerable.Repeat("_", parameters.Length))}) => {canExecuteName}()" 
                        : $", ({string.Join(", ", Enumerable.Repeat("_", parameters.Length))}) => {canExecuteName}";
                }
            }
            
            code.AppendMultiline(
                $"""
                 {GeneratedAttribute}
                 private {type} {fieldName};

                 {GeneratedAttribute}
                 private {type} {propertyName} => {fieldName} ??= new {type}({methodName}{canExecuteName});

                 """);
        }

        return code;
    }
}