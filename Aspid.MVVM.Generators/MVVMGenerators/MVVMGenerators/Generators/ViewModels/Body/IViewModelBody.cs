using MVVMGenerators.Helpers;
using MVVMGenerators.Helpers.Descriptions;
using MVVMGenerators.Helpers.Extensions.Writer;
using MVVMGenerators.Generators.ViewModels.Data;
using MVVMGenerators.Helpers.Extensions.Symbols;
using MVVMGenerators.Generators.ViewModels.Data.Members;

namespace MVVMGenerators.Generators.ViewModels.Body;

// ReSharper disable InconsistentNaming
public static class IViewModelBody
{
    private const string GeneratedAttribute = General.GeneratedCodeViewModelAttribute;

    private static readonly string IBinder = Classes.IBinder.Global;
    private static readonly string Exception = Classes.Exception.Global;
    private static readonly string BindResult = Classes.BindResult.Global;
    private static readonly string ViewModelEvent = Classes.ViewModelEvent.Global;
    private static readonly string ProfilerMarker = Classes.ProfilerMarker.Global;
    private static readonly string EditorBrowsableAttribute = $"[{Classes.EditorBrowsableAttribute.Global}({Classes.EditorBrowsableState.Global}.Never)]";

    public static CodeWriter AppendIViewModelBody(this CodeWriter code, ViewModelDataSpan data)
    {
        if (data.Inheritor == Inheritor.None)
        {
            code.AppendProfilerMarkers(in data)
                .AppendLine()
                .AppendAddBinder()
                .AppendLine()
                .AppendAddBinderInternal(in data)
                .AppendLine()
                .AppendManualMethods();
        }
        else
        {
            code.AppendAddBinderInternal(in data)
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
             #if !{Defines.ASPID_MVVM_UNITY_PROFILER_DISABLED}
             {EditorBrowsableAttribute}
             {GeneratedAttribute}
             private static readonly {ProfilerMarker} __addBinderMarker = new("{className}.AddBinder"); 
             #endif
             """);

        return code;
    }

    private static CodeWriter AppendAddBinder(this CodeWriter code)
    {
        code.AppendMultiline(
            $$"""
            {{GeneratedAttribute}}
            public {{BindResult}} AddBinder({{IBinder}} binder, string propertyName)
            {
                #if !{{Defines.ASPID_MVVM_UNITY_PROFILER_DISABLED}}
                using (__addBinderMarker.Auto())
                #endif
                {
                    return AddBinderInternal(binder, propertyName);
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
             protected {{additionalModificator}} {{BindResult}} AddBinderInternal({{IBinder}} binder, string propertyName)
             {
                 switch (propertyName)
             """)
            .IncreaseIndent()
            .BeginBlock()
            .AppendLoop(data.Fields, AppendField)
            .AppendLoop(data.Commands, AppendCommand)
            .AppendLoop(data.BindAlsoProperties, AppendBindAlsoProperty)
            .AppendMultiline(
            $$"""
            default:
            {
                {{BindResult}} result = default;
                AddBinderManual(binder, propertyName, ref result);
                if (result.IsBound) return result;
                
                break;
            }
            """)
            .EndBlock()
            .AppendLine()
            .AppendLineIf(data.Inheritor is Inheritor.InheritorViewModelAttribute,
                "return base.AddBinderInternal(binder, propertyName);")
            .AppendLineIf(data.Inheritor is not Inheritor.InheritorViewModelAttribute, "return default;")
            .AppendMultilineIf(readOnlyFieldsExist,
            $$"""
            
            void SetValueLocal<T>(T value)
            {
                if (binder is not {{IBinder}}<T> specificBinder)
                    throw new {{Exception}}($"Binder ({binder.GetType()}) is not {typeof({{IBinder}}<T>)}");
                    
                specificBinder.SetValue(value);
            }
            """)
            .EndBlock();

        return code;
        
        void AppendCommand(RelayCommandData command) =>
            AppendReadOnlyBind(command.PropertyName);

        void AppendField(FieldInViewModel field)
        {
            var type = field.Type.ToDisplayStringGlobal();
            var propertyName = field.PropertyName;
            
            if (!field.IsReadOnly)
            {
                code.AppendMultiline(
                    $$"""
                    case {{propertyName}}Id:
                    {
                        var isReverse = binder.IsReverseEnabled;
                        {{field.ViewModelEventName}} ??= new {{ViewModelEvent}}<{{type}}>();
                        
                        if (isReverse)
                            {{field.ViewModelEventName}}.SetValue ??= Set{{propertyName}};
                            
                        return new({{field.ViewModelEventName}}.AddBinder(binder, {{propertyName}}, isReverse));
                    }
                    """);
            }
            else AppendReadOnlyBind(propertyName);
        }

        void AppendBindAlsoProperty(BindAlsoProperty property)
        {
            var type = property.Type.ToDisplayStringGlobal();
            
            code.AppendMultiline(
                $$"""
                case {{property.Name}}Id:
                {
                    {{property.ViewModelEventName}} ??= new {{ViewModelEvent}}<{{type}}>();
                    return new({{property.ViewModelEventName}}.AddBinder(binder, {{property.Name}}, false));
                }
                """);
        }

        void AppendReadOnlyBind(string propertyName)
        {
            readOnlyFieldsExist = true;

            code.AppendMultiline(
                $$"""
                  case {{propertyName}}Id:
                  {
                      SetValueLocal({{propertyName}});
                      return new(true);
                  }
                  """);
        }
    }

    private static CodeWriter AppendManualMethods(this CodeWriter code)
    {
        code.AppendLine(GeneratedAttribute)
            .AppendLine($"partial void AddBinderManual({IBinder} binder, string propertyName, ref {BindResult} result);");

        return code;
    }
}