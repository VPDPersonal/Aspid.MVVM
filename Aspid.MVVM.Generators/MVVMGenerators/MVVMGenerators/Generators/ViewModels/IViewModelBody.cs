using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers;
using System.Collections.Generic;
using System.Linq;
using MVVMGenerators.Helpers.Descriptions;
using MVVMGenerators.Helpers.Extensions.Writer;
using MVVMGenerators.Helpers.Extensions.Symbols;

namespace MVVMGenerators.Generators.ViewModels;

// ReSharper disable once InconsistentNaming
public static class IViewModelBody
{
    private const string BinderVar = "binder";
    private const string ChangedVar = "changed";
    private const string PropertyNameVar = "propertyName";
    private const string SpecificBinderVar = "specificBinder";
    private const string SpecificReverseBinderVar = "specificReverseBinder";

    private const string SetValueMethod = "SetValue";
    private const string AddBindersMethod = "AddBinder";
    private const string AddBindersLocalMethod = "AddBinderLocal";
    private const string AddReverseBindersLocalMethod = "AddReverseBinderLocal";
    private const string AddBindersMethodManual = "AddBinderManual";
    private const string AddBindersIternalMethod = "AddBinderIternal";
    private const string RemoveBindersMethod = "RemoveBinder";
    private const string RemoveBindersLocalMethod = "RemoveBinderLocal";
    private const string RemoveReverseBindersLocalMethod = "RemoveReverseBinderLocal";
    private const string RemoveBindersMethodManual = "RemoveBinderManual";
    private const string RemoveBindersIternalMethod = "RemoveBinderIternal";
    
    public static CodeWriter AppendIViewModel(this CodeWriter code, bool hasBaseType, bool hasInterface, string className, in IEnumerable<IFieldSymbol> fields)
    {
        return code
            .AppendMultilineIf(!hasBaseType && !hasInterface,
                $"""
                 #if !{Defines.ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED}
                 {General.GeneratedCodeViewModelAttribute}
                 private static readonly {Classes.ProfilerMarker.Global} _addBinderMarker = new("{className}.{AddBindersMethod}");
                 {General.GeneratedCodeViewModelAttribute}
                 private static readonly {Classes.ProfilerMarker.Global} _removeBinderMarker = new("{className}.{RemoveBindersMethod}");
                 #endif
                 
                 """)
            .AppendAddBinder(hasBaseType, hasInterface, fields)
            .AppendLine()
            .AppendRemoveBinder(hasBaseType, hasInterface, fields)
            .AppendLine()
            .AppendManualMethods();
    }
    
    private static CodeWriter AppendAddBinder(this CodeWriter code, bool hasBaseType, bool hasInterface, in IEnumerable<IFieldSymbol> fields)
    {
        var virtualOrOverride = !hasBaseType ? "virtual" : "override";
        
        return 
            code.AppendMultilineIf(!hasBaseType && !hasInterface, 
                $$"""
                {{General.GeneratedCodeViewModelAttribute}}
                public void {{AddBindersMethod}}({{Classes.IBinder.Global}} {{BinderVar}}, string {{PropertyNameVar}})
                {
                    #if !{{Defines.ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED}}
                    using (_addBinderMarker.Auto())
                    #endif
                    {
                        {{AddBindersIternalMethod}}({{BinderVar}}, {{PropertyNameVar}});
                    }
                }
                
                """)
            .AppendMultiline(
                $$"""
                {{General.GeneratedCodeViewModelAttribute}}
                protected {{virtualOrOverride}} void {{AddBindersIternalMethod}}({{Classes.IBinder.Global}} {{BinderVar}}, string {{PropertyNameVar}})
                {
                    switch ({{PropertyNameVar}})
                    {
                """)
            .IncreaseIndent()
            .IncreaseIndent()
            .AppendLoop(fields, field =>
            {
                var propertyName = field.GetPropertyName();
                
                code.AppendMultiline(
                    $"""
                    case {propertyName}Id: 
                        {AddBindersLocalMethod}({propertyName}, ref {propertyName}Changed);
                        if ({BinderVar}.IsReverseEnabled) {AddReverseBindersLocalMethod}<{field.Type}>(Set{propertyName});
                        return;
                    """);
            })
            .AppendMultiline(
                $"""
                 default:
                    var flag = false;
                    {AddBindersMethodManual}({BinderVar}, {PropertyNameVar}, ref flag);
                    if (flag) return;
                    break;
                 """)
            .EndBlock()
            .AppendLineIf(hasBaseType, $"base.{AddBindersIternalMethod}({BinderVar}, {PropertyNameVar});")
            .AppendMultilineIf(fields.Any(),
                $$"""
                return;
            
                void {{AddBindersLocalMethod}}<T>(T value, ref {{Classes.Action.Global}}<T> {{ChangedVar}})
                {   
                    if ({{BinderVar}} is not {{Classes.IBinder.Global}}<T> {{SpecificBinderVar}})
                        throw new {{Classes.Exception.Global}}();
                        
                    {{SpecificBinderVar}}.{{SetValueMethod}}(value);
                    {{ChangedVar}} += {{SpecificBinderVar}}.{{SetValueMethod}};
                }
                
                void {{AddReverseBindersLocalMethod}}<T>({{Classes.Action.Global}}<T> setValue)
                {
                    if ({{BinderVar}} is not {{Classes.IReverseBinder.Global}}<T> {{SpecificReverseBinderVar}})
                        throw new {{Classes.Exception.Global}}();
                        
                    {{SpecificReverseBinderVar}}.ValueChanged += setValue;
                }
                """)
            .EndBlock();
    }

    private static CodeWriter AppendRemoveBinder(this CodeWriter code, bool hasBaseType, bool hasInterface, in IEnumerable<IFieldSymbol> fields)
    {
        var virtualOrOverride = !hasBaseType ? "virtual" : "override";
        
        return 
            code.AppendMultilineIf(!hasBaseType && !hasInterface,
                $$"""
                {{General.GeneratedCodeViewModelAttribute}}
                public void {{RemoveBindersMethod}}({{Classes.IBinder.Global}} {{BinderVar}}, string {{PropertyNameVar}}) 
                {
                    #if !{{Defines.ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED}}
                    using (_removeBinderMarker.Auto())
                    #endif
                    {
                        {{RemoveBindersIternalMethod}}({{BinderVar}}, {{PropertyNameVar}});
                    }
                }
                
                """)
            .AppendMultiline(
                $$"""
                {{General.GeneratedCodeViewModelAttribute}}
                protected {{virtualOrOverride}} void {{RemoveBindersIternalMethod}}({{Classes.IBinder.Global}} {{BinderVar}}, string {{PropertyNameVar}})
                {
                    switch ({{PropertyNameVar}})
                    {    
                """)
            .IncreaseIndent()
            .IncreaseIndent()
            .AppendLoop(fields, field => 
            {
                var propertyName = field.GetPropertyName();
                code.AppendMultiline(
                    $"""
                    case {propertyName}Id: 
                        {RemoveBindersLocalMethod}(ref {propertyName}Changed);
                        if ({BinderVar}.IsReverseEnabled) {RemoveReverseBindersLocalMethod}<{field.Type}>(Set{propertyName});
                        return;
                    """);
            })
            .AppendMultiline(
                $"""
                 default:
                    var flag = false;
                    {RemoveBindersMethodManual}({BinderVar}, {PropertyNameVar}, ref flag);
                    if (flag) return;
                    break;
                 """)
            .EndBlock()
            .AppendLineIf(hasBaseType, $"base.{RemoveBindersIternalMethod}({BinderVar}, {PropertyNameVar});")
            .AppendMultilineIf(fields.Any(),
                $$"""
                return;
                
                void {{RemoveBindersLocalMethod}}<T>(ref {{Classes.Action.Global}}<T> {{ChangedVar}})
                {
                    if ({{BinderVar}} is not {{Classes.IBinder.Global}}<T> {{SpecificBinderVar}})
                        throw new {{Classes.Exception.Global}}();
                        
                    {{ChangedVar}} -= {{SpecificBinderVar}}.{{SetValueMethod}};
                }      
                
                void {{RemoveReverseBindersLocalMethod}}<T>({{Classes.Action.Global}}<T> setValue)
                {
                    if ({{BinderVar}} is not {{Classes.IReverseBinder.Global}}<T> {{SpecificReverseBinderVar}})
                        throw new {{Classes.Exception.Global}}();
                        
                    {{SpecificReverseBinderVar}}.ValueChanged -= setValue;
                }
                """)
            .EndBlock();
    }

    private static CodeWriter AppendManualMethods(this CodeWriter code)
    {
        return code.AppendMultiline(
            $"""
            partial void {AddBindersMethodManual}({Classes.IBinder.Global} {BinderVar}, string {PropertyNameVar}, ref bool flag);
            
            partial void {RemoveBindersMethodManual}({Classes.IBinder.Global} {BinderVar}, string {PropertyNameVar}, ref bool flag);
            """);
    }
}