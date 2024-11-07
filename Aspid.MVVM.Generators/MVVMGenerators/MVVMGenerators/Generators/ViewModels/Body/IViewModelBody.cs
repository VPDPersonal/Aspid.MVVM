using System;
using MVVMGenerators.Helpers;
using MVVMGenerators.Helpers.Descriptions;
using MVVMGenerators.Helpers.Extensions.Writer;
using MVVMGenerators.Generators.ViewModels.Data;
using MVVMGenerators.Generators.ViewModels.Data.Members;

namespace MVVMGenerators.Generators.ViewModels.Body;

// ReSharper disable InconsistentNaming
public static class IViewModelBody
{
    private const string GeneratedAttribute = General.GeneratedCodeViewModelAttribute;

    private static readonly string Action = Classes.Action.Global;
    private static readonly string IBinder = Classes.IBinder.Global;
    private static readonly string Exception = Classes.Exception.Global;
    private static readonly string IReverseBinder = Classes.IReverseBinder.Global;
    private static readonly string ProfilerMarker = Classes.ProfilerMarker.Global;

    public static CodeWriter AppendIViewModelBody(this CodeWriter code, in ViewModelDataSpan data)
    {
        if (data.Inheritor == Inheritor.None)
        {
            code.AppendProfilerMarkers(in data)
                .AppendLine()
                .AppendAddBinder()
                .AppendLine()
                .AppendAddBinderInternal(in data)
                .AppendLine()
                .AppendRemoveBinder()
                .AppendLine()
                .AppendRemoveBinderInternal(in data)
                .AppendLine()
                .AppendManualMethods();
        }
        else
        {
            code.AppendAddBinderInternal(in data)
                .AppendLine()
                .AppendRemoveBinderInternal(in data)
                .AppendLine()
                .AppendManualMethods();
        }

        return code;
    }

    private static CodeWriter AppendProfilerMarkers(this CodeWriter code, in ViewModelDataSpan data)
    {
        var className = data.Declaration.Identifier.Text;
        
        code.AppendMultiline(
            $"""
            #if !{Defines.ASPID_UI_MVVM_UNITY_PROFILER_DISABLED}
            {GeneratedAttribute}
            private static readonly {ProfilerMarker} _addBinderMarker = new("{className}.AddBinder"); 
            
            {GeneratedAttribute}
            private static readonly {ProfilerMarker} _removeBinderMarker = new("{className}.RemoveBinder");
            #endif
            """);
        
        return code;
    }

    private static CodeWriter AppendAddBinder(this CodeWriter code)
    {
        code.AppendMultiline(
            $$"""
            {{GeneratedAttribute}}
            public void AddBinder({{IBinder}} binder, string propertyName)
            {
                #if !{{Defines.ASPID_UI_MVVM_UNITY_PROFILER_DISABLED}}
                using (_addBinderMarker.Auto())
                #endif
                {
                    AddBinderInternal(binder, propertyName);
                }
            }
            """);
        
        return code;
    }

    private static CodeWriter AppendAddBinderInternal(this CodeWriter code, in ViewModelDataSpan data)
    { 
        var readOnlyFieldsExist = false;
        var additionalModificator = data.HasBaseType
            ? "override" 
            : "virtual";

        code.AppendMultiline(
            $$"""
            protected {{additionalModificator}} void AddBinderInternal({{IBinder}} binder, string propertyName)
            {
                switch (propertyName)
            """)
            .IncreaseIndent()
            .BeginBlock()
            .AppendLoop(data.Fields, AppendField)
            .AppendLoop(data.Commands, AppendCommand)
            .AppendMultiline(
            """
            default:
            {
                var isAdded = false;
                AddBinderManual(binder, propertyName, ref isAdded);
                if (isAdded) return;
                break;
            }
            """)
            .EndBlock()
            .AppendLineIf(data.Inheritor is Inheritor.InheritorViewModelAttribute, "base.AddBinderInternal(binder, propertyName);");

        AppendLocalMethods(in data.Fields);
        code.EndBlock();
        
        return code;

        void AppendField(FieldInViewModel field)
        {
            var type = field.Type;
            var propertyName = field.PropertyName;

            if (field.IsReadOnly)
            {
                readOnlyFieldsExist = true;

                code.AppendMultiline(
                    $$"""
                      case {{propertyName}}Id:
                      {
                          SetValueLocal({{propertyName}});
                          return;
                      }
                      """);
            }
            else
            {
                code.AppendMultiline(
                    $$"""
                      case {{propertyName}}Id:
                      {
                          AddBinderLocal({{propertyName}}, ref {{propertyName}}Changed);
                          if (binder.IsReverseEnabled) AddReverseBinderLocal<{{type}}>(Set{{propertyName}});
                          return;
                      }
                      """);
            }
        }

        void AppendCommand(RelayCommandData command)
        {
            readOnlyFieldsExist = true;
            var propertyName = command.PropertyName;
                
            code.AppendMultiline(
                $$"""
                case {{propertyName}}Id:
                {
                    SetValueLocal({{propertyName}});
                    return;
                }
                """);
        }
        
        void AppendLocalMethods(in ReadOnlySpan<FieldInViewModel> fields)
        {
            code.AppendMultilineIf(fields.Length > 0,
                $$"""
                
                return;
                
                void AddBinderLocal<T>(T value, ref {{Action}}<T> changed)
                {   
                    if (binder is not {{IBinder}}<T> specificBinder)
                        throw new {{Exception}}($"binder ({binder.GetType()}) is not {typeof({{IBinder}}<T>)}");
                        
                    specificBinder.SetValue(value);
                    changed += specificBinder.SetValue;
                }
                
                void AddReverseBinderLocal<T>({{Action}}<T> setValue)
                {
                    if (binder is not {{IReverseBinder}}<T> specificReverseBinder)
                        throw new {{Exception}}($"binder ({binder.GetType()}) is not {typeof({{IReverseBinder}}<T>)}");
                        
                    specificReverseBinder.ValueChanged += setValue;
                }
                """)
                .AppendMultilineIf(readOnlyFieldsExist,
                $$"""
                
                void SetValueLocal<T>(T value)
                {
                    if (binder is not {{IBinder}}<T> specificBinder)
                        throw new {{Exception}}($"binder ({binder.GetType()}) is not {typeof({{IBinder}}<T>)}");
                        
                    specificBinder.SetValue(value);
                }
                """);

            return;
        }
    }
    
    private static CodeWriter AppendRemoveBinder(this CodeWriter code)
    {
        code.AppendMultiline(
            $$"""
            {{GeneratedAttribute}}
            public void RemoveBinder({{IBinder}} binder, string propertyName)
            {
                #if !{{Defines.ASPID_UI_MVVM_UNITY_PROFILER_DISABLED}}
                using (_removeBinderMarker.Auto())
                #endif
                {
                    RemoveBinderInternal(binder, propertyName);
                }
            }
            """);

        return code;
    }

    private static CodeWriter AppendRemoveBinderInternal(this CodeWriter code, in ViewModelDataSpan data)
    {
        var additionalModificator = data.HasBaseType ? "override" : "virtual";

        code.AppendMultiline(
            $$"""
            {{GeneratedAttribute}}
            protected {{additionalModificator}} void RemoveBinderInternal({{IBinder}} binder, string propertyName)
            {
                switch (propertyName)
            """)
            .IncreaseIndent()
            .BeginBlock()
            .AppendLoop(data.Fields, AppendField)
            .AppendMultiline(
            """
            default:
            {
                var isRemoved = false;
                RemoveBinderManual(binder, propertyName, ref isRemoved);
                if (isRemoved) return;
                break;
            }
            """)
            .EndBlock()
            .AppendLineIf(data.Inheritor is Inheritor.InheritorViewModelAttribute, "base.RemoveBinderInternal(binder, propertyName);");
        
        AppendLocalMethods(data.Fields);
        code.EndBlock();
        
        return code;

        void AppendField(FieldInViewModel field)
        {
            if (field.IsReadOnly) return;

            var type = field.Type;
            var propertyName = field.PropertyName;

            code.AppendMultiline(
                $$"""
                case {{propertyName}}Id:
                {
                    RemoveBinderLocal(ref {{propertyName}}Changed);
                    if (binder.IsReverseEnabled) RemoveReverseBinderLocal<{{type}}>(Set{{propertyName}});
                    return;
                }
                """);
        }
        
        void AppendLocalMethods(in ReadOnlySpan<FieldInViewModel> fields)
        {
            code.AppendMultilineIf(fields.Length != 0,
                $$"""
                
                return;
                
                void RemoveBinderLocal<T>(ref {{Action}}<T> changed)
                {   
                    if (binder is not {{IBinder}}<T> specificBinder)
                        throw new {{Exception}}($"binder ({binder.GetType()}) is not {typeof({{IBinder}}<T>)}");
                        
                    changed -= specificBinder.SetValue;
                }
                
                void RemoveReverseBinderLocal<T>({{Action}}<T> setValue)
                {
                    if (binder is not {{IReverseBinder}}<T> specificReverseBinder)
                        throw new {{Exception}}($"binder ({binder.GetType()}) is not {typeof({{IReverseBinder}}<T>)}");
                        
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