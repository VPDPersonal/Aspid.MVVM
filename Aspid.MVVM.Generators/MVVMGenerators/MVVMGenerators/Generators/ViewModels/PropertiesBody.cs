using System;
using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers;
using MVVMGenerators.Helpers.Descriptions;
using MVVMGenerators.Helpers.Extensions.Writer;
using MVVMGenerators.Helpers.Extensions.Symbols;

namespace MVVMGenerators.Generators.ViewModels;

public static class PropertiesBody
{
    public static CodeWriter AppendViewModelProperties(this CodeWriter code, ReadOnlySpan<IFieldSymbol> fields)
    {
        return code
            .AppendLoop(fields, code.AppendEvents)
            .AppendLine()
            .AppendLoop(fields, code.AppendProperty)
            .AppendLoop(fields, code.AppendPartialMethods);
    }

    private static void AppendEvents(this CodeWriter code, IFieldSymbol field)
    {
        code
            .AppendLine(General.GeneratedCodeViewModelAttribute)
            .AppendLine($"public event {Classes.Action.Global}<{field.Type}> {field.GetPropertyName()}Changed;");
    }

    private static void AppendProperty(this CodeWriter code, IFieldSymbol field)
    {
        var type = field.Type;
        var name = field.Name;
        var propertyName = field.GetPropertyName();

        code
            .AppendMultiline(
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
                          On{{propertyName}}Changed(value);
                          {{propertyName}}Changed?.Invoke({{name}});
                      }
                  }
                  
                  """);
    }

    private static void AppendPartialMethods(this CodeWriter code, IFieldSymbol field)
    {
        var type = field.Type;
        var name = field.GetPropertyName();

        code
            .AppendMultiline(
                $"""
                 {General.GeneratedCodeViewModelAttribute}
                 partial void On{name}Changing({type} oldValue, {type} newValue);

                 {General.GeneratedCodeViewModelAttribute}
                 partial void On{name}Changed({type} newValue);

                 """);
    }
}