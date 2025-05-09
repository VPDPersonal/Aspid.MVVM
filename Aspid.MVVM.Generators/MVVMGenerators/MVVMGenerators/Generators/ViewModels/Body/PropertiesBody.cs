using System;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers;
using MVVMGenerators.Helpers.Data;
using MVVMGenerators.Helpers.Descriptions;
using MVVMGenerators.Helpers.Extensions.Writer;
using MVVMGenerators.Generators.ViewModels.Data;
using MVVMGenerators.Generators.ViewModels.Data.Members;

namespace MVVMGenerators.Generators.ViewModels.Body;

public static class PropertiesBody
{
    // ReSharper disable once InconsistentNaming
    private static readonly string EditorBrowsableAttribute = $"[{Classes.EditorBrowsableAttribute.Global}({Classes.EditorBrowsableState.Global}.Never)]";

    public static void Generate(
        string @namespace,
        in ViewModelDataSpan data,
        in DeclarationText declaration,
        in SourceProductionContext context)
    {
        if (data.Members.IsEmpty) return;
        
        var code = new CodeWriter();

        code.AppendClassBegin(@namespace, declaration)
            .AppendPropertiesBody(data)
            .AppendClassEnd(@namespace);

        context.AddSource(declaration.GetFileName(@namespace, "Properties"), code.GetSourceText());
    }

    private static CodeWriter AppendPropertiesBody(this CodeWriter code, ViewModelDataSpan data)
    {
        var fields = data.MembersByType.Fields;
        
        code.AppendEvents(data.Members)
            .AppendViewModelEvents(data.Members)
            .AppendProperties(fields)
            .AppendSetMethods(fields);
        
        return code;
    }

    #region Events
    private static CodeWriter AppendEvents(
        this CodeWriter code, 
        in ImmutableArray<BindableMember> bindableMembers)
    {
        foreach (var bindableMember in bindableMembers)
        {
            if (bindableMember.Mode is BindMode.OneTime or BindMode.OneWayToSource) continue;

            switch (bindableMember)
            {
                case BindableField bindableField:
                    {
                        code.AppendEvent(bindableMember, bindableField.Event)
                            .AppendLine();
                        break;
                    }
                
                case BindableBindAlso bindableBindAlso:
                    {
                        code.AppendEvent(bindableMember, bindableBindAlso.Event)
                            .AppendLine();
                        break;
                    }
            }
        }
        
        return code;
    }

    private static CodeWriter AppendEvent(this CodeWriter code, BindableMember member, in ViewModelEvent viewModelEvent)
    {
        var eventName = viewModelEvent.Name!;
        var type = viewModelEvent.Type!;
        var eventFieldName = viewModelEvent.FieldName!;
        var eventType = viewModelEvent.EventType!;
        var parameters = member.Mode is BindMode.TwoWay
            ? $"Set{member.GeneratedName}"
            : string.Empty;

        return code.AppendMultiline(
            $$"""
              {{General.GeneratedCodeViewModelAttribute}}
              public event {{Classes.Action.Global}}<{{type}}> {{eventName}}
              {
                  add
                  {
                      {{eventFieldName}} ??= new {{eventType}}<{{type}}>({{parameters}});
                      {{eventFieldName}}.Changed += value;
                  }
                  remove
                  {
                      if ({{eventFieldName}} is null) return;
                      {{eventFieldName}}.Changed -= value;
                  }
              }
              """);
    }
    #endregion

    #region ViewModel Events
    private static CodeWriter AppendViewModelEvents(
        this CodeWriter code,
        in ImmutableArray<BindableMember> bindableMembers)
    {
        foreach (var bindableMember in bindableMembers)
        {
            if (bindableMember.Mode is BindMode.OneTime) continue;

            switch (bindableMember)
            {
                case BindableField bindableField:
                    {
                        code.AppendViewModelEvent(bindableField.Event)
                            .AppendLine();
                        break;
                    }
                
                case BindableBindAlso bindableBindAlso:
                    {
                        code.AppendViewModelEvent(bindableBindAlso.Event)
                            .AppendLine();
                        break;
                    }
            }
        }
        return code;
    }

    private static CodeWriter AppendViewModelEvent(this CodeWriter code, in ViewModelEvent bindableField)
    {
        return !bindableField.Has 
            ? code 
            : code.AppendViewModelEvent(bindableField.Type!, bindableField.EventType!, bindableField.FieldName!);
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
    private static CodeWriter AppendProperties(this CodeWriter code, in CastedSpan<BindableMember, BindableField> bindableFields)
    {
        foreach (var field in bindableFields)
            code.AppendProperty(field)
                .AppendLine();
        
        return code;
    }

    private static CodeWriter AppendProperty(this CodeWriter code, in BindableField bindableField)
    {
        if (bindableField.IsReadOnly)
        {
            return code.AppendMultiline(
                $"""
                {General.GeneratedCodeViewModelAttribute}
                {bindableField.GeneralAccessAsText}{bindableField.Type} {bindableField.GeneratedName} => {bindableField.SourceName};
                """
            );
        }
        
        return code.AppendMultiline(
            $$"""
            {{General.GeneratedCodeViewModelAttribute}}
            {{bindableField.GeneralAccessAsText}}{{bindableField.Type}} {{bindableField.GeneratedName}}
            {
                {{bindableField.GetAccessAsText}}get => {{bindableField.SourceName}};
                {{bindableField.SetAccessAsText}}set => Set{{bindableField.GeneratedName}}(value);
            }
            """
        );
    }
    #endregion

    #region Set Methods
    private static void AppendSetMethods(this CodeWriter code, in CastedSpan<BindableMember, BindableField> bindableFields)
    {
        foreach (var bindableField in bindableFields)
        {
            if (bindableField.Mode is BindMode.OneTime) continue;
            
            code.AppendSetMethod(bindableField)
                .AppendLine();
        }
    }

    private static CodeWriter AppendSetMethod(this CodeWriter code, in BindableField bindableField)
    {
        if (bindableField.Mode is BindMode.OneTime) return code;
        
        var changedMethod = $"On{bindableField.GeneratedName}Changed";
        var changingMethod = $"On{bindableField.GeneratedName}Changing";
        
        var eventInvoke = bindableField.Event.Has && bindableField.Mode is not BindMode.OneWayToSource 
            ? $"\n\t{bindableField.Event.FieldName}?.Invoke({bindableField.SourceName});" 
            : string.Empty;

        foreach (var property in bindableField.BindAlso)
            eventInvoke += $"\n\t{property.Event.FieldName}?.Invoke({property.SourceName});";
        
        return code.AppendMultiline(
                $$"""
                {{General.GeneratedCodeViewModelAttribute}}
                private void Set{{bindableField.GeneratedName}}({{bindableField.Type}} value)
                {
                    if ({{Classes.EqualityComparer.Global}}<{{bindableField.Type}}>.Default.Equals({{bindableField.SourceName}}, value)) return;
                    
                    {{changingMethod}}({{bindableField.SourceName}}, value);
                    {{bindableField.SourceName}} = value;
                    {{changedMethod}}(value);{{eventInvoke}}
                }
                """
        )
        .AppendLine()
        .AppendMultiline(
            $"""
            {General.GeneratedCodeViewModelAttribute}
            partial void {changingMethod}({bindableField.Type} oldValue, {bindableField.Type} newValue);

            {General.GeneratedCodeViewModelAttribute}
            partial void {changedMethod}({bindableField.Type} newValue);
            """
        );
    }
    #endregion
}