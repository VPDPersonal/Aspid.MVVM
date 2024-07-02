using System;
using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers;
using System.Collections.Generic;
using MVVMGenerators.Descriptions;
using MVVMGenerators.Extensions.Symbols;

namespace MVVMGenerators.Generators.ViewModels;

// ReSharper disable once InconsistentNaming
public static class IViewModelBodyCodeWriterExtensions
{
    private const string BindersVar = "binders";
    
    public static void AppendGetBindMethods(this CodeWriter code, IReadOnlyCollection<IFieldSymbol> fields)
    {
        AppendGetMethod(code, "bindMethods", AppendBindMethodForBind, fields);
        AppendGetMethod(code, "unbindMethods", AppendBindMethodForUnbind, fields);
    }

    private static void AppendGetMethod(
        CodeWriter code,
        string methodName, 
        Action<CodeWriter, IFieldSymbol> method,
        IReadOnlyCollection<IFieldSymbol> fields)
    {
        var iViewModel = Classes.IViewModel.Global;
        var bindMethods = Classes.BindMethods.Global;
        var iReadOnlyBindsMethods = Classes.IReadOnlyBindsMethods.Global;

        var upperMethodName = char.ToUpper(methodName[0]) + methodName.Remove(0, 1);
        var getMethodName = $"Get{upperMethodName}";
        var addMethodName = $"Add{upperMethodName}";
        
        code.AppendLine(General.GeneratedCodeAttribute)
            .AppendLine($"{iReadOnlyBindsMethods} {iViewModel}.{getMethodName}() => {getMethodName}();")
            .AppendLine()
            .AppendLine(General.GeneratedCodeAttribute)
            .AppendLine($"protected virtual {iReadOnlyBindsMethods} {getMethodName}()")
            .BeginBlock()
            .AppendLine($"var {methodName} = new {bindMethods}")
            .BeginBlock();

        foreach (var field in fields)
        {
            code.Append("{ ");
            method.Invoke(code, field);
            code.AppendLine(" },");
        }
        
        code.DecreaseIndent()
            .AppendLine("};")
            .AppendLine()
            .AppendLine($"{addMethodName}(ref {methodName});")
            .AppendLine($"return {methodName};")
            .EndBlock()
            .AppendLine()
            .AppendLine(General.GeneratedCodeAttribute)
            .AppendLine($"partial void {addMethodName}(ref {bindMethods} {methodName});")
            .AppendLine();
    }

    private static void AppendBindMethodForBind(CodeWriter code, IFieldSymbol field)
    {
        var name = field.Name;
        var propertyName = field.GetPropertyName();
        var changedName = $"{propertyName}Changed";
        var bindMethod = $"{Classes.ViewModelUtility.Global}.Bind";

        code.Append($"nameof({propertyName}), {BindersVar} => {bindMethod}({name}, ref {changedName}, {BindersVar})");
    }

    private static void AppendBindMethodForUnbind(CodeWriter code, IFieldSymbol field)
    {
        var propertyName = field.GetPropertyName();
        var changedName = $"{propertyName}Changed";
        var bindMethod = $"{Classes.ViewModelUtility.Global}.Unbind";

        code.Append($"nameof({propertyName}), {BindersVar} => {bindMethod}(ref {changedName}, {BindersVar})");
    }
}