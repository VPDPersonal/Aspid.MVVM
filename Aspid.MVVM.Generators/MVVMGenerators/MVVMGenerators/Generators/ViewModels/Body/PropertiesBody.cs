using System;
using MVVMGenerators.Helpers;
using MVVMGenerators.Helpers.Descriptions;
using MVVMGenerators.Generators.ViewModels.Data;
using MVVMGenerators.Helpers.Extensions.Symbols;
using MVVMGenerators.Generators.ViewModels.Data.Members;
using MVVMGenerators.Generators.ViewModels.Data.Members.Fields;

namespace MVVMGenerators.Generators.ViewModels.Body;

public static class PropertiesBody
{
    // ReSharper disable once InconsistentNaming
    private static readonly string EditorBrowsableAttribute = $"[{Classes.EditorBrowsableAttribute.Global}({Classes.EditorBrowsableState.Global}.Never)]";
    
    public static CodeWriter AppendPropertiesBody(this CodeWriter code, ViewModelDataSpan data)
    {
        var fields = data.Fields;
        var properties = data.BindAlsoProperties;
        
        code
            .AppendEvents(fields, properties)
            .AppendViewModelEvents(fields, properties)
            .AppendProperties(fields)
            .AppendSetMethods(fields);
        
        return code;
    }

    #region Events
    private static CodeWriter AppendEvents(
        this CodeWriter code, 
        in ViewModelFieldsSpan fields, 
        in ReadOnlySpan<BindAlsoProperty> bindAlsoProperties)
    {
        foreach (var field in fields.OneWayFields)
            code.AppendEvent(field)
                .AppendLine();
        
        foreach (var field in fields.TwoWayFields)
            code.AppendEvent(field)
                .AppendLine();
        
        foreach (var property in bindAlsoProperties)
            code.AppendEvent(property.Type.ToDisplayStringGlobal(), Classes.OneWayViewModelEvent.Global, property.EventName, property.ViewModelEventName)
                .AppendLine();
        
        return code;
    }

    private static CodeWriter AppendEvent(this CodeWriter code, in ViewModelField field)
    {
        if (field.Mode is BindMode.None or BindMode.OneTime or BindMode.OneWayToSource) return code;
        return code.AppendEvent(field.Type, field.Event.EventType!, field.Event.Name!, field.Event.FieldName!);
    }
    
    private static CodeWriter AppendEvent(
        this CodeWriter code, 
        string type, 
        string eventTYpe,
        string eventName,
        string eventFieldName)
    {
        return code.AppendMultiline(
            $$"""
            {{General.GeneratedCodeViewModelAttribute}}
            public event {{Classes.Action.Global}}<{{type}}> {{eventName}}
            {
                add
                {
                    {{eventFieldName}} ??= new {{eventTYpe}}<{{type}}>();
                    {{eventFieldName}}.Changed += value;
                }
                remove
                {
                    if ({{eventFieldName}} is null) return;
                    {{eventFieldName}}.Changed -= value;
                }
            }
            """
        );
    }
    #endregion

    #region ViewModel Events
    private static CodeWriter AppendViewModelEvents(
        this CodeWriter code,
        in ViewModelFieldsSpan fields, 
        in ReadOnlySpan<BindAlsoProperty> bindAlsoProperties)
    {
        foreach (var field in fields.OneWayFields)
            code.AppendViewModelEvent(field)
                .AppendLine();
        
        foreach (var field in fields.TwoWayFields)
            code.AppendViewModelEvent(field)
                .AppendLine();
        
        foreach (var field in fields.OneWayToSourceFields)
            code.AppendViewModelEvent(field)
                .AppendLine();

        foreach (var property in bindAlsoProperties)
            code.AppendViewModelEvent(property.Type.ToDisplayStringGlobal(), Classes.OneWayViewModelEvent.Global, property.ViewModelEventName);

        return code;
    }

    private static CodeWriter AppendViewModelEvent(this CodeWriter code, in ViewModelField field)
    {
        if (!field.Event.Has) return code;
        return code.AppendViewModelEvent(field.Type, field.Event.EventType!, field.Event.FieldName!);
    }
    
    private static CodeWriter AppendViewModelEvent(this CodeWriter code, string type, string eventType, string eventFieldName)
    {
        return code.AppendMultiline(
            $"""
            {EditorBrowsableAttribute}
            {General.GeneratedCodeViewModelAttribute}
            private {eventType}<{type}> {eventFieldName};
            """
        );
    }
    #endregion

    #region Properties
    private static CodeWriter AppendProperties(this CodeWriter code, in ViewModelFieldsSpan fields)
    {
        foreach (var field in fields.All)
            code.AppendProperty(field)
                .AppendLine();
        
        return code;
    }

    private static CodeWriter AppendProperty(this CodeWriter code, in ViewModelField field)
    {
        if (field.IsReadOnly)
        {
            return code.AppendMultiline(
                $"""
                {General.GeneratedCodeViewModelAttribute}
                {field.GeneralAccessAsText}{field.Type} {field.PropertyName} => {field.FieldName};
                """
            );
        }
        
        return code.AppendMultiline(
            $$"""
            {{General.GeneratedCodeViewModelAttribute}}
            {{field.GeneralAccessAsText}}{{field.Type}} {{field.PropertyName}}
            {
                {{field.GetAccessAsText}}get => {{field.FieldName}};
                {{field.SetAccessAsText}}set => Set{{field.PropertyName}}(value);
            }
            """
        );
    }
    #endregion

    #region Set Methods
    private static void AppendSetMethods(this CodeWriter code, in ViewModelFieldsSpan fields)
    {
        foreach (var field in fields.OneWayFields)
            code.AppendSetMethod(field)
                .AppendLine();
        
        foreach (var field in fields.TwoWayFields)
            code.AppendSetMethod(field)
                .AppendLine();
        
        foreach (var field in fields.OneWayToSourceFields)
            code.AppendSetMethod(field)
                .AppendLine();
    }

    private static CodeWriter AppendSetMethod(this CodeWriter code, in ViewModelField field)
    {
        if (field.Mode is BindMode.None or BindMode.OneTime) return code;
        
        var changedMethod = $"On{field.PropertyName}Changed";
        var changingMethod = $"On{field.PropertyName}Changing";
        
        var eventInvoke = field.Event.Has && field.Mode is not BindMode.OneWayToSource 
            ? $"\n\t{field.Event.FieldName}?.Invoke({field.FieldName});" 
            : string.Empty;

        foreach (var property in field.BindAlso)
            eventInvoke += $"\n\t{property.ViewModelEventName}?.Invoke({property.Name});";
        
        return code.AppendMultiline(
                $$"""
                {{General.GeneratedCodeViewModelAttribute}}
                private void Set{{field.PropertyName}}({{field.Type}} value)
                {
                    if ({{Classes.EqualityComparer.Global}}<{{field.Type}}>.Default.Equals({{field.FieldName}}, value)) return;
                    
                    {{changingMethod}}({{field.FieldName}}, value);
                    {{field.FieldName}} = value;
                    {{changedMethod}}(value);{{eventInvoke}}
                }
                """
        )
        .AppendLine()
        .AppendMultiline(
            $"""
            {General.GeneratedCodeViewModelAttribute}
            partial void {changingMethod}({field.Type} oldValue, {field.Type} newValue);

            {General.GeneratedCodeViewModelAttribute}
            partial void {changedMethod}({field.Type} newValue);
            """
        );
    }
  #endregion
}