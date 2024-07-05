using System;
using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers;
using System.Collections.Generic;
using MVVMGenerators.Descriptions;
using MVVMGenerators.Extensions.Symbols;

namespace MVVMGenerators.Generators.ViewModels;

// ReSharper disable once InconsistentNaming
public static class IViewModelBody
{
    private const string BindersVar = "binders";

    public static CodeWriter AppendGetBindMethods(
        this CodeWriter code,
        bool hasBaseType, 
        bool hasInterface, 
        IReadOnlyCollection<IFieldSymbol> fields)
    {
        AppendGetMethod(code, "bindMethods", hasBaseType, hasInterface, GetBindMethodForBind, fields);
        AppendGetMethod(code, "unbindMethods", hasBaseType, hasInterface, GetBindMethodForUnbind, fields);
        return code;
    }

    private static void AppendGetMethod(
        CodeWriter code,
        string methodName, 
        bool hasBaseType,
        bool hasInterface,
        Func<IFieldSymbol, string> method,
        IReadOnlyCollection<IFieldSymbol> fields)
    {
        var iViewModelType = Classes.IViewModel.Global;
        var bindMethodsType = Classes.BindMethods.Global;
        var iReadOnlyBindsMethodsType = Classes.IReadOnlyBindsMethods.Global;

        var upperMethodName = char.ToUpper(methodName[0]) + methodName.Remove(0, 1);
        var getMethodName = $"Get{upperMethodName}";
        var addMethodName = $"Add{upperMethodName}";

        if (!hasBaseType && !hasInterface)
        {
            code.AppendLine(General.GeneratedCodeAttribute)
                .AppendLine($"{iReadOnlyBindsMethodsType} {iViewModelType}.{getMethodName}() => {getMethodName}Iternal();")
                .AppendLine();
        }

        var methodIternal = hasBaseType ? "protected override " : "protected virtual ";
        methodIternal += $"{bindMethodsType} {getMethodName}Iternal()";
        
        code.AppendLine(General.GeneratedCodeAttribute)
            .AppendLine(methodIternal)
            .BeginBlock();

        if (hasBaseType)
        {
            code.AppendLine($"var {methodName} = base.{getMethodName}Iternal();");

            foreach (var field in fields)
                code.AppendLine($"{methodName}.Add({method.Invoke(field)});");
        }
        else
        {
            code.AppendLine($"var {methodName} = new {bindMethodsType}")
                .BeginBlock();
            
            foreach (var field in fields)
                code.AppendLine($"{{ {method.Invoke(field)} }},");
            
            code.DecreaseIndent()
                .AppendLine("};");
        }
        
        code.AppendLine()
            .AppendLine($"{addMethodName}(ref {methodName});")
            .AppendLine($"return {methodName};")
            .EndBlock()
            .AppendLine()
            .AppendLine(General.GeneratedCodeAttribute)
            .AppendLine($"partial void {addMethodName}(ref {bindMethodsType} {methodName});")
            .AppendLine();
    }

    private static string GetBindMethodForBind(IFieldSymbol field)
    {
        var name = field.Name;
        var propertyName = field.GetPropertyName();
        var changedName = $"{propertyName}Changed";
        var bindMethod = $"{Classes.ViewModelUtility.Global}.Bind";

        return $"nameof({propertyName}), {BindersVar} => {bindMethod}({name}, ref {changedName}, {BindersVar})";
    }

    private static string GetBindMethodForUnbind(IFieldSymbol field)
    {
        var propertyName = field.GetPropertyName();
        var changedName = $"{propertyName}Changed";
        var bindMethod = $"{Classes.ViewModelUtility.Global}.Unbind";

        return $"nameof({propertyName}), {BindersVar} => {bindMethod}(ref {changedName}, {BindersVar})";
    }
}