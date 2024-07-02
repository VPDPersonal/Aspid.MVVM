using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers;
using System.Collections.Generic;
using MVVMGenerators.Descriptions;
using MVVMGenerators.Extensions.Symbols;

namespace MVVMGenerators.Generators.ViewModels;

public static class PropertiesBodyCodeWriterExtension
{
    public static void AppendViewModelProperties(this CodeWriter code, IReadOnlyCollection<IFieldSymbol> fields)
    {
        if (fields.Count == 0) return;

        foreach (var field in fields)
            AppendEvents(code, field);

        code.AppendLine();
        
        foreach (var field in fields)
            AppendProperty(code, field);
        
        foreach (var field in fields)
            AppendPartialMethods(code, field);
    }

    private static void AppendEvents(CodeWriter code, IFieldSymbol field) =>
        code.AppendLine($"public event {Classes.Action.Global}<{field.Type}> {field.GetPropertyName()}Changed;");

    private static void AppendProperty(CodeWriter code, IFieldSymbol field)
    {
        var propertyName = field.GetPropertyName();
        
        code.AppendLine(General.GeneratedCodeAttribute)
            .AppendLine($"private {field.Type} {propertyName}")
            .BeginBlock()
            .AppendLine($"get => {field.Name};")
            .AppendLine("set")
            .BeginBlock()
            .AppendLine($"if ({Classes.ViewModelUtility.Global}.EqualsDefault({field.Name}, value))")
            .BeginBlock()
            .AppendLine($"On{propertyName}Changing({field.Name}, value);")
            .AppendLine($"{field.Name} = value;")
            .AppendLine($"{propertyName}Changed?.Invoke({field.Name});")
            .AppendLine($"On{propertyName}Changed(value);")
            .EndBlock()
            .EndBlock()
            .EndBlock()
            .AppendLine();
    }

    private static void AppendPartialMethods(CodeWriter code, IFieldSymbol field)
    {
        var type = field.Type;
        var propertyName = field.GetPropertyName();
        
        code.AppendLine(General.GeneratedCodeAttribute)
            .AppendLine($"partial void On{propertyName}Changing({type} oldValue, {type} newValue);")
            .AppendLine()
            .AppendLine(General.GeneratedCodeAttribute)
            .AppendLine($"partial void On{propertyName}Changed({type} newValue);")
            .AppendLine();
    }
}