using MVVMGenerators.Helpers;
using MVVMGenerators.Helpers.Descriptions;
using MVVMGenerators.Helpers.Extensions.Writer;
using MVVMGenerators.Generators.ViewModels.Data;
using MVVMGenerators.Helpers.Extensions.Symbols;
using MVVMGenerators.Generators.ViewModels.Data.Members;
using MVVMGenerators.Generators.ViewModels.Data.Members.Fields;

namespace MVVMGenerators.Generators.ViewModels.Body;

// ReSharper disable InconsistentNaming
public static class IViewModelBody
{
    private const string GeneratedAttribute = General.GeneratedCodeViewModelAttribute;

    private static readonly string IBinder = Classes.IBinder.Global;
    private static readonly string BindMode = Classes.BindMode.Global;
    private static readonly string Exception = Classes.Exception.Global;
    private static readonly string BindResult = Classes.BindResult.Global;
    private static readonly string ProfilerMarker = Classes.ProfilerMarker.Global;
    private static readonly string OneWayViewModelEvent = Classes.OneWayViewModelEvent.Global;
    private static readonly string TwoWayViewModelEvent = Classes.TwoWayViewModelEvent.Global;
    private static readonly string OneWayToSourceViewModelEvent = Classes.OneWayToSourceViewModelEvent.Global;
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
            .AppendFields(data.Fields)
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
            .EndBlock();

        return code;
        
        void AppendCommand(RelayCommandData command) =>
            code.AppendOneTimeFieldInSwitch(command.GetTypeName(), command.PropertyName);

        void AppendBindAlsoProperty(BindAlsoProperty property)
        {
            var type = property.Type.ToDisplayStringGlobal();
            code.AppendOneWayFieldInSwitch(type, property.Name, property.ViewModelEventName);
        }
    }

    private static CodeWriter AppendFields(this CodeWriter code, ViewModelFieldsSpan fields)
    {
        foreach (var field in fields.TwoWayFields)
            code.AppendTwoWayFieldInSwitch(field);

        foreach (var field in fields.OneWayFields)
            code.AppendOneWayFieldInSwitch(field.Type, field.PropertyName, field.Event.FieldName!);
                
        foreach (var field in fields.OneTimeFields)
            code.AppendOneTimeFieldInSwitch(field.Type, field.PropertyName);
        
        foreach (var field in fields.OneWayToSourceFields)
            code.AppendOneWayToSourceFieldInSwitch(field);

        return code;
    }

    private static CodeWriter AppendTwoWayFieldInSwitch(this CodeWriter code, in ViewModelField field)
    {
        return code.AppendMultiline(
            $$"""
            case {{field.PropertyName}}Id:
            {
                var mode = binder.Mode;
                
                {{field.Event.FieldName}} ??= new {{TwoWayViewModelEvent}}<{{field.Type}}>();
                
                if (mode is {{BindMode}}.TwoWay or {{BindMode}}.OneWayToSource)
                    {{field.Event.FieldName}}.SetValue ??= Set{{field.PropertyName}};
                    
                return new({{field.Event.FieldName}}.AddBinder(binder, {{field.PropertyName}}, mode));
            }
            """
        );
    }
    
    private static CodeWriter AppendOneWayFieldInSwitch(this CodeWriter code, string type, string propertyName, string evenName)
    {
        return code.AppendMultiline(
            $$"""
            case {{propertyName}}Id:
            {
                {{evenName}} ??= new {{OneWayViewModelEvent}}<{{type}}>();
                return new({{evenName}}.AddBinder(binder, {{propertyName}}));
            }
            """
        );
    }

    private static CodeWriter AppendOneTimeFieldInSwitch(this CodeWriter code, string type, string propertyName)
    {
        return code.AppendMultiline(
            $$"""
            case {{propertyName}}Id:
            {
                if (binder.Mode is {{BindMode}}.TwoWay or {{BindMode}}.OneWayToSource)
                    throw new {{Classes.Exception.Global}}();
                
                if (binder is not {{IBinder}}<{{type}}> specificBinder)
                    throw new {{Exception}}($"Binder ({binder.GetType()}) is not {typeof({{IBinder}}<{{type}}>)}");
                
                specificBinder.SetValue({{propertyName}});
                return new(true);
            }
            """
        );
    }

    private static CodeWriter AppendOneWayToSourceFieldInSwitch(this CodeWriter code, in ViewModelField field)
    {
        return code.AppendMultiline(
            $$"""
            case {{field.PropertyName}}Id:
            {
                {{field.Event.FieldName}} ??= new {{OneWayToSourceViewModelEvent}}<{{field.Type}}>(Set{{field.PropertyName}});
                return new({{field.Event.FieldName}}.AddBinder(binder));
            }
            """);
    }

    private static CodeWriter AppendManualMethods(this CodeWriter code)
    {
        code.AppendLine(GeneratedAttribute)
            .AppendLine($"partial void AddBinderManual({IBinder} binder, string propertyName, ref {BindResult} result);");

        return code;
    }
}