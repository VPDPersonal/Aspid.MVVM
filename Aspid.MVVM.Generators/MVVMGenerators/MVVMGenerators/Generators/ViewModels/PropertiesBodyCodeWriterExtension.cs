using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers;
using System.Collections.Generic;
using MVVMGenerators.Extensions.Symbols;

namespace MVVMGenerators.Generators.ViewModels;

public static class PropertiesBodyCodeWriterExtension
{
    public static void AppendViewModelProperties(this CodeWriter writer, IReadOnlyCollection<IFieldSymbol> fields)
    {
        if (fields.Count == 0) return;

        foreach (var field in fields)
            AppendEvents(writer, field);

        writer.AppendLine();
        
        foreach (var field in fields)
            AppendProperty(writer, field);
        
        foreach (var field in fields)
            AppendPartialMethods(writer, field);
    }

    private static void AppendEvents(CodeWriter writer, IFieldSymbol field) =>
        writer.AppendLine($"public event global::System.Action<{field.Type}> {field.GetPropertyName()}Changed;");

    private static void AppendProperty(CodeWriter writer, IFieldSymbol field)
    {
        var propertyName = field.GetPropertyName();
        
        writer
            .AppendLine("[global::System.CodeDom.Compiler.GeneratedCode(\"UltimateUI.Mvvm.SourceGenerators.ViewModelGenerator\", \"0.0.1\")]")
            .AppendLine($"private {field.Type} {propertyName}")
            .BeginBlock()
            .AppendLine($"get => {field.Name};")
            .AppendLine("set")
            .BeginBlock()
            .AppendLine($"if (global::UltimateUI.MVVM.ViewModels.ViewModelUtility.EqualsDefault({field.Name}, value))")
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

    private static void AppendPartialMethods(CodeWriter writer, IFieldSymbol field)
    {
        var type = field.Type;
        var propertyName = field.GetPropertyName();
        
        writer
            .AppendLine("[global::System.CodeDom.Compiler.GeneratedCode(\"UltimateUI.Mvvm.SourceGenerators.ViewModelGenerator\", \"0.0.1\")]")
            .AppendLine($"partial void On{propertyName}Changing({type} oldValue, {type} newValue);")
            .AppendLine()
            .AppendLine("[global::System.CodeDom.Compiler.GeneratedCode(\"UltimateUI.Mvvm.SourceGenerators.ViewModelGenerator\", \"0.0.1\")]")
            .AppendLine($"partial void On{propertyName}Changed({type} newValue);")
            .AppendLine();
    }
}