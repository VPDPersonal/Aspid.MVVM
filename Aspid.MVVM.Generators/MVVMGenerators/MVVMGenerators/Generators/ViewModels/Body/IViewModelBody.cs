using System;
using MVVMGenerators.Helpers;
using MVVMGenerators.Helpers.Descriptions;
using MVVMGenerators.Helpers.Extensions.Writer;
using MVVMGenerators.Generators.ViewModels.Data;

namespace MVVMGenerators.Generators.ViewModels.Body;

// ReSharper disable once InconsistentNaming
public static class IViewModelBody
{
    private const string GeneratedAttribute = General.GeneratedCodeViewModelAttribute;

    private static readonly string Action = Classes.Action.Global;
    private static readonly string IBinder = Classes.IBinder.Global;
    private static readonly string Exception = Classes.Exception.Global;
    private static readonly string IReverseBinder = Classes.IReverseBinder.Global;
    private static readonly string ProfilerMarker = Classes.ProfilerMarker.Global;

    public static CodeWriter AppendIViewModelBody(this CodeWriter code, ViewModelData data)
    {
        var dataSpan = new ViewModelDataSpan(data);

        code.AppendProfilerMarkers(dataSpan)
            .AppendLine()
            .AppendAddBinder(dataSpan)
            .AppendLine()
            .AppendRemoveBinder(dataSpan)
            .AppendLine()
            .AppendManualMethods();

        return code;
    }

    private static CodeWriter AppendProfilerMarkers(this CodeWriter code, in ViewModelDataSpan data)
    {
        var className = data.Declaration.Identifier.Text;
        
        code.AppendMultiline(
            $"""
            #if !{Defines.ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED}
            {GeneratedAttribute}
            private static readonly {ProfilerMarker} _addBinderMarker = new("{className}.AddBinder"); 
            
            {GeneratedAttribute}
            private static readonly {ProfilerMarker} _removeBinderMarker = new("{className}.RemoveBinder");
            #endif
            """);
        
        return code;
    }

    private static CodeWriter AppendAddBinder(this CodeWriter code, in ViewModelDataSpan data)
    {
        var hasBaseType = data.HasViewModelBaseType;
        var hasInterface = data.HasViewModelInterface;
        
        AppendBaseMethodIf(!hasBaseType && !hasInterface);
        AppendSwitch(data.Fields);
        code.AppendLineIf(hasBaseType, "base.AddBinderInternal(binder, propertyName);");
        AppendLocalMethods(data.Fields);
        code.EndBlock();

        return code;

        void AppendBaseMethodIf(bool isAppend)
        {
            code.AppendMultilineIf(isAppend,
                $$"""
                {{GeneratedAttribute}}
                public void AddBinder({{IBinder}} binder, string propertyName)
                {
                    #if !{{Defines.ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED}}
                    using (_addBinderMarker.Auto())
                    #endif
                    {
                        AddBinderInternal(binder, propertyName);
                    }
                }
                
                """);
        }

        void AppendSwitch(in ReadOnlySpan<FieldData> fields)
        {
            var additionalModificator = hasBaseType ? "override" : "virtual";
            
            code.AppendMultiline(
                $$"""
                {{GeneratedAttribute}}
                protected {{additionalModificator}} void AddBinderInternal({{IBinder}} binder, string propertyName)
                {
                    switch (propertyName)
                    {
                """)
            .IncreaseIndent()
            .IncreaseIndent()
            .AppendLoop(fields, field =>
            {
                var type = field.Type;
                var propertyName = field.PropertyName;
                
                code.AppendMultiline(
                    $"""
                    case {propertyName}Id:
                        AddBinderLocal({propertyName}, ref {propertyName}Changed);
                        if (binder.IsReverseEnabled) AddReverseBinderLocal<{type}>(Set{propertyName});
                        return;
                    """);
            })
            .AppendMultiline(
                """
                default:
                    var isAdded = false;
                    AddBinderManual(binder, propertyName, ref isAdded);
                    if (isAdded) return;
                    break;
                """)
            .EndBlock();
        }

        void AppendLocalMethods(in ReadOnlySpan<FieldData> fields)
        {
            code.AppendMultilineIf(fields.Length > 0,
                $$"""
                
                return;
                
                void AddBinderLocal<T>(T value, ref {{Action}}<T> changed)
                {   
                    if (binder is not {{IBinder}}<T> specificBinder)
                        throw new {{Exception}}();
                        
                    specificBinder.SetValue(value);
                    changed += specificBinder.SetValue;
                }
                
                void AddReverseBinderLocal<T>({{Action}}<T> setValue)
                {
                    if (binder is not {{IReverseBinder}}<T> specificReverseBinder)
                        throw new {{Exception}}();
                        
                    specificReverseBinder.ValueChanged += setValue;
                }
                """);
        }
    }
    
    private static CodeWriter AppendRemoveBinder(this CodeWriter code, in ViewModelDataSpan data)
    {
        var hasBaseType = data.HasViewModelBaseType;
        var hasInterface = data.HasViewModelInterface;
        
        AppendBaseMethodsIf(!hasBaseType && !hasInterface);
        AppendSwitch(data.Fields);
        code.AppendLineIf(hasBaseType, "base.RemoveBinderIternal(binder, propertyName);");
        AppendLocalMethods(data.Fields);
        code.EndBlock();
        
        return code;

        void AppendBaseMethodsIf(bool isAppend)
        {
            code.AppendMultilineIf(isAppend,
                $$"""
                {{GeneratedAttribute}}
                public void RemoveBinder({{IBinder}} binder, string propertyName)
                {
                    #if !{{Defines.ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED}}
                    using (_removeBinderMarker.Auto())
                    #endif
                    {
                        RemoveBinderIternal(binder, propertyName);
                    }
                }
                
                """);
        }

        void AppendSwitch(in ReadOnlySpan<FieldData> fields)
        {
            var additionalModificator = hasBaseType ? "override" : "virtual";
            
            code.AppendMultiline(
                    $$"""
                      {{GeneratedAttribute}}
                      protected {{additionalModificator}} void RemoveBinderIternal({{IBinder}} binder, string propertyName)
                      {
                          switch (propertyName)
                          {
                      """)
                .IncreaseIndent()
                .IncreaseIndent()
                .AppendLoop(fields, field =>
                {
                    var type = field.Type;
                    var propertyName = field.PropertyName;
                
                    code.AppendMultiline(
                        $"""
                         case {propertyName}Id:
                             RemoveBinderLocal(ref {propertyName}Changed);
                             if (binder.IsReverseEnabled) RemoveReverseBinderLocal<{type}>(Set{propertyName});
                             return;
                         """);
                })
                .AppendMultiline(
                    """
                    default:
                        var isRemoved = false;
                        RemoveBinderManual(binder, propertyName, ref isRemoved);
                        if (isRemoved) return;
                        break;
                    """)
                .EndBlock();
        }

        void AppendLocalMethods(in ReadOnlySpan<FieldData> fields)
        {
            code.AppendMultilineIf(fields.Length > 0,
                $$"""
                
                return;
                
                void RemoveBinderLocal<T>(ref {{Action}}<T> changed)
                {   
                    if (binder is not {{IBinder}}<T> specificBinder)
                        throw new {{Exception}}();
                        
                    changed -= specificBinder.SetValue;
                }
                
                void RemoveReverseBinderLocal<T>({{Action}}<T> setValue)
                {
                    if (binder is not {{IReverseBinder}}<T> specificReverseBinder)
                        throw new {{Exception}}();
                        
                    specificReverseBinder.ValueChanged -= setValue;
                }
                """);
        }
    }

    private static CodeWriter AppendManualMethods(this CodeWriter code)
    {
        code.AppendMultiline(
            $"""
            {GeneratedAttribute}
            partial void AddBinderManual({IBinder} binder, string propertyName, ref bool isAdded);
            
            {GeneratedAttribute}
            partial void RemoveBinderManual({IBinder} binder, string propertyName, ref bool isRemoved);
            """);
        
        return code;
    }
}