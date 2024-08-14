using System;
using MVVMGenerators.Helpers;
using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers.Descriptions;
using MVVMGenerators.Helpers.Extensions.Writer;

namespace MVVMGenerators.Generators.ViewModels.Body;

public static class PropertiesBody
{
    public static CodeWriter AppendPropertiesBody(this CodeWriter code, ViewModelDataSpan data)
    {
        code.AppendEvents(data)
            .AppendProperties(data)
            .AppendSetMethods(data);
        
        return code;
    }

    private static CodeWriter AppendEvents(this CodeWriter code, in ViewModelDataSpan data)
    {
        code.AppendLoop(data.Fields, field =>
        {
            code.AppendMultiline(
                $"""
                {General.GeneratedCodeViewModelAttribute}
                public event {Classes.Action.Global}<{field.Type}> {field.PropertyName}Changed;
                
                """);
        });

        return code;
    }

    private static CodeWriter AppendProperties(this CodeWriter code, in ViewModelDataSpan data)
    {
        code.AppendLoop(data.Fields, field =>
        {
            AppendProperty(field.Type, field.Name, field.PropertyName, field.GetAccess, field.SetAccess);
        });
        
        code.AppendLoop(data.Commands, command =>
        {
            // var propertyName = 
            //
            // // TODO IRelay Type
            // AppendProperty(null, PropertySymbolExtensions.GetFieldNameFromPropertyName(command.ExecuteMethod.Name), 
            //     field.PropertyName, field.GetAccess, field.SetAccess);
        });
        
        return code;

        void AppendProperty(ITypeSymbol type, string name, string propertyName, int getAccess, int setAccess)
        {
            var getAccessName = "";
            var setAccessName = "";
            var generalAccessName = GetAccess(Math.Max(0, Math.Max(getAccess, setAccess)));
            
            if (getAccess > setAccess) setAccessName = GetAccess(setAccess);
            else if (getAccess < setAccess) getAccessName = GetAccess(getAccess);
            
            code.AppendMultiline(
                $$"""
                  {{General.GeneratedCodeViewModelAttribute}}
                  {{generalAccessName}}{{type}} {{propertyName}}
                  {
                      {{getAccessName}}get => {{name}};
                      {{setAccessName}}set => Set{{propertyName}}(value);
                  }
                  
                  """);
        }

        static string GetAccess(int number) => number switch
        {
            0 => "private ",
            1 => "protected ",
            2 => "public ",
            _ => ""
        };
    }

    private static void AppendSetMethods(this CodeWriter code, in ViewModelDataSpan data)
    {
        var fieldCount = data.Fields.Length;
        
        code.AppendLoop(data.Fields, (i, field) =>
        {
            var type = field.Type;
            var name = field.Name;
            var propertyName = field.PropertyName;

            var changedMethod = $"On{propertyName}Changed";
            var changingMethod = $"On{propertyName}Changing";
            
            /*lang=C#*/
            code.AppendMultiline(
                $$"""
                {{General.GeneratedCodeViewModelAttribute}}
                private void Set{{propertyName}}({{type}} value)
                {
                    {{changingMethod}}({{name}}, value);
                    {{name}} = value;
                    {{changedMethod}}(value);
                    {{propertyName}}Changed?.Invoke({{name}});
                }

                """);
                
            code.AppendMultiline(
                $"""
                {General.GeneratedCodeViewModelAttribute}
                partial void {changingMethod}({type} oldValue, {type} newValue);

                {General.GeneratedCodeViewModelAttribute}
                partial void {changedMethod}({type} newValue);
                """);

            code.AppendLineIf(i + 1 < fieldCount);
        });
    }
}