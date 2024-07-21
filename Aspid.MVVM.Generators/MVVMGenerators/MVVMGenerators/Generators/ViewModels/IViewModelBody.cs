using System;
using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers;
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

    private const string SetValueMethod = "SetValue";
    private const string AddBindersMethod = "AddBinder";
    private const string AddBindersLocalMethod = "AddBinderLocal";
    private const string AddBindersIternalMethod = "AddBinderIternal";
    private const string RemoveBindersMethod = "RemoveBinder";
    private const string RemoveBindersLocalMethod = "RemoveBinderLocal";
    private const string RemoveBindersIternalMethod = "RemoveBinderIternal";
    
    public static CodeWriter AppendIViewModel(this CodeWriter code, bool hasBaseType, bool hasInterface, string classname, in ReadOnlySpan<IFieldSymbol> fields)
    {
        return code
            .AppendMultilineIf(!hasBaseType && !hasInterface,
                $"""
                 #if !{Defines.ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED}
                 {General.GeneratedCodeViewModelAttribute}
                 private static readonly {Classes.ProfilerMarker.Global} _addBinderMarker = new("{classname}.{AddBindersMethod}");
                 {General.GeneratedCodeViewModelAttribute}
                 private static readonly {Classes.ProfilerMarker.Global} _removeBinderMarker = new("{classname}.{RemoveBindersMethod}");
                 #endif
                 
                 """)
            .AppendAddBinder(hasBaseType, hasInterface, fields)
            .AppendLine()
            .AppendRemoveBinder(hasBaseType, hasInterface, fields);
    }
    
    private static CodeWriter AppendAddBinder(this CodeWriter code, bool hasBaseType, bool hasInterface, in ReadOnlySpan<IFieldSymbol> fields)
    {
        // TODO Add inheritors support
        return code
            .AppendMultilineIf(!hasBaseType && !hasInterface, 
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
                protected virtual void {{AddBindersIternalMethod}}({{Classes.IBinder.Global}} {{BinderVar}}, string {{PropertyNameVar}})
                {
                    switch ({{PropertyNameVar}})
                    {
                """)
            .IncreaseIndent()
            .IncreaseIndent()
            .AppendLoop(fields, field =>
            {
                var propertyName = field.GetPropertyName();
                code.AppendLine($"case {propertyName}Id: {AddBindersLocalMethod}({propertyName}, ref {propertyName}Changed); break;");
            })
            .EndBlock()
            .AppendMultiline(
                $$"""
                return;
            
                void {{AddBindersLocalMethod}}<T>(T value, ref {{Classes.Action.Global}}<T> {{ChangedVar}})
                {           
                    if ({{BinderVar}} is not {{Classes.IBinder.Global}}<T> {{SpecificBinderVar}})
                        throw new {{Classes.Exception.Global}}();
                
                    {{SpecificBinderVar}}.{{SetValueMethod}}(value);
                    {{ChangedVar}} += {{SpecificBinderVar}}.{{SetValueMethod}};
                }
                """)
            .EndBlock();
    }

    private static CodeWriter AppendRemoveBinder(this CodeWriter code, bool hasBaseType, bool hasInterface, in ReadOnlySpan<IFieldSymbol> fields)
    {
        // TODO Add inheritors support
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
                protected virtual void {{RemoveBindersIternalMethod}}({{Classes.IBinder.Global}} {{BinderVar}}, string {{PropertyNameVar}})
                {
                    switch ({{PropertyNameVar}})
                    {    
                """)
            .IncreaseIndent()
            .IncreaseIndent()
            .AppendLoop(fields, field => 
            {
                var propertyName = field.GetPropertyName();
                code.AppendLine($"case {propertyName}Id: {RemoveBindersLocalMethod}(ref {propertyName}Changed); break;");
            })
            .EndBlock()
            .AppendMultiline(
                $$"""
                return;
                
                void {{RemoveBindersLocalMethod}}<T>(ref {{Classes.Action.Global}}<T> {{ChangedVar}})
                {
                    if ({{BinderVar}} is not {{Classes.IBinder.Global}}<T> {{SpecificBinderVar}})
                        throw new {{Classes.Exception.Global}}();
                        
                    {{ChangedVar}} -= {{SpecificBinderVar}}.{{SetValueMethod}};
                }      
                """)
            .EndBlock();
    }
}