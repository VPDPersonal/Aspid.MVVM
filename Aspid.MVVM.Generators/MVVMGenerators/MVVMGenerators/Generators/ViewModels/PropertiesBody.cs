using System;
using MVVMGenerators.Helpers;
using System.Collections.Generic;
using MVVMGenerators.Helpers.Descriptions;
using MVVMGenerators.Helpers.Extensions.Writer;
using MVVMGenerators.Helpers.Extensions.Symbols;

namespace MVVMGenerators.Generators.ViewModels;

public static class PropertiesBody
{
    public static CodeWriter AppendViewModelProperties(this CodeWriter code, IReadOnlyCollection<FieldData> fields)
    {
        return code
            .AppendLoop(fields, code.AppendEvents)
            .AppendLine()
            .AppendLoop(fields, code.AppendProperty)
            .AppendLoop(fields, code.AppendPartialMethods);
    }

    private static void AppendEvents(this CodeWriter code, FieldData field)
    {
        var type = field.Field.Type;
        var name = field.Field.GetPropertyName();
        
        code.AppendLine(General.GeneratedCodeViewModelAttribute)
            .AppendLine($"public event {Classes.Action.Global}<{type}> {name}Changed;");
    }

    private static void AppendProperty(this CodeWriter code, FieldData field)
    {
        var type = field.Field.Type;
        var name = field.Field.Name;
        var propertyName = field.Field.GetPropertyName();

        var getAccess = "";
        var setAccess = "";
        var generalAccess = GetAccess(Math.Max(0, Math.Max(field.GetAccess, field.SetAccess)));

        if (field.GetAccess > field.SetAccess) setAccess = GetAccess(field.SetAccess);
        else if (field.GetAccess < field.SetAccess) getAccess = GetAccess(field.GetAccess);
        
        code.AppendMultiline(
                $$"""
                  {{General.GeneratedCodeViewModelAttribute}}
                  {{generalAccess}}{{type}} {{propertyName}}
                  {
                      {{getAccess}}get => {{name}};
                      {{setAccess}}set 
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

    private static void AppendPartialMethods(this CodeWriter code, FieldData field)
    {
        var type = field.Field.Type;
        var name = field.Field.GetPropertyName();

        code
            .AppendMultiline(
                $"""
                 {General.GeneratedCodeViewModelAttribute}
                 partial void On{name}Changing({type} oldValue, {type} newValue);

                 {General.GeneratedCodeViewModelAttribute}
                 partial void On{name}Changed({type} newValue);

                 """);
    }

    private static string GetAccess(int number) => number switch
    {
        0 => "private ",
        1 => "protected ",
        2 => "public ",
        _ => ""
    };
}