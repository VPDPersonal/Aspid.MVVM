using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers;
using System.Collections.Generic;
using MVVMGenerators.Descriptions;
using MVVMGenerators.Extensions.Symbols;

namespace MVVMGenerators.Generators.ViewModels;

public static class PropertiesBody
{
    public static void AppendViewModelProperties(this CodeWriter code, IReadOnlyCollection<IFieldSymbol> fields)
    {
        foreach (var field in fields)
            AppendEvents(code, field);

        code.AppendLine();
        
        foreach (var field in fields)
            AppendProperty(code, field);
        
        foreach (var field in fields)
            AppendPartialMethods(code, field);
    }

    private static void AppendEvents(CodeWriter code, IFieldSymbol field)
    {
        code.AppendLine(General.GeneratedCodeViewModelAttribute)
            .AppendLine($"public event {Classes.Action.Global}<{field.Type}> {field.GetPropertyName()}Changed;");
    }

    private static void AppendProperty(CodeWriter code, IFieldSymbol field)
    {
        var type = field.Type;
        var name = field.Name;
        var propertyName = field.GetPropertyName();
        
        code.AppendMultiline(
            $$"""
            {{General.GeneratedCodeViewModelAttribute}}
            private {{type}} {{propertyName}}
            {
                get => {{field.Name}};
                set 
                {
                    if ({{Classes.ViewModelUtility.Global}}.EqualsDefault({{name}}, value)) return;
                    
                    On{{propertyName}}Changing({{name}}, value);
                    {{name}} = value;
                    On{{propertyName}}Changed(value);;
                    {{propertyName}}Changed?.Invoke({{name}});
                }
            }
            """);

        code.AppendLine();
    }

    private static void AppendPartialMethods(CodeWriter code, IFieldSymbol field)
    {
        var type = field.Type;
        var name = field.GetPropertyName();
        
        code.AppendMultiline(
            $"""
              {General.GeneratedCodeViewModelAttribute}
              partial void On{name}Changing({type} oldValue, {type} newValue);
              
              {General.GeneratedCodeViewModelAttribute}
              partial void On{name}Changed({type} newValue);
              """);

        code.AppendLine();
    }
}