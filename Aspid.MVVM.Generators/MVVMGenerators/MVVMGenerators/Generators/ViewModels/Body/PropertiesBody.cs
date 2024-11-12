using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp;
using MVVMGenerators.Helpers.Descriptions;
using MVVMGenerators.Helpers.Extensions.Writer;
using MVVMGenerators.Generators.ViewModels.Data;
using MVVMGenerators.Helpers.Extensions.Symbols;

namespace MVVMGenerators.Generators.ViewModels.Body;

public static class PropertiesBody
{
    public static CodeWriter AppendPropertiesBody(this CodeWriter code, ViewModelDataSpan data)
    {
        code.AppendEvents(data)
            .AppendViewModelEvents(data)
            .AppendLine()
            .AppendProperties(data)
            .AppendSetMethods(data);
        
        return code;
    }

    private static CodeWriter AppendEvents(this CodeWriter code, in ViewModelDataSpan data)
    {
        HashSet<IPropertySymbol> bindAlsoProperties = [];

        foreach (var field in data.Fields)
        {
            if (field.IsReadOnly) continue;
            
            foreach (var property in field.BindAlso)
                bindAlsoProperties.Add(property);
            
            AppendEvent(field.Type, field.EventName, field.ViewModelEventName);
        }

        foreach (var property in bindAlsoProperties)
        {
            var type = property.Type;
            var eventName = $"{property.Name}Changed";
            var viewModelEventName = $"__{property.GetFieldName(false)}ChangedEvent";
            AppendEvent(type, eventName, viewModelEventName);
        }

        return code;

        void AppendEvent(ITypeSymbol type, string eventName, string viewModelEventName)
        {
            code.AppendMultiline(
                $$"""
                {{General.GeneratedCodeViewModelAttribute}}
                public event {{Classes.Action.Global}}<{{type}}> {{eventName}}
                {
                    add
                    {
                        {{viewModelEventName}} ??= new {{Classes.ViewModelEvent.Global}}<{{type}}>();
                        {{viewModelEventName}}.Changed += value;
                    }
                    remove
                    {
                        if ({{viewModelEventName}} is null) return;
                        {{viewModelEventName}}.Changed -= value;
                    }
                }
                
                """);
        }
    }

    private static CodeWriter AppendViewModelEvents(this CodeWriter code, in ViewModelDataSpan data)
    {
        HashSet<IPropertySymbol> bindAlsoProperties = [];
        
        foreach (var field in data.Fields)
        {
            if (field.IsReadOnly) continue;
            
            foreach (var property in field.BindAlso)
                bindAlsoProperties.Add(property);
            
            code.AppendMultiline(
                $"""
                {General.GeneratedCodeViewModelAttribute}
                private {Classes.ViewModelEvent.Global}<{field.Type}> {field.ViewModelEventName};
                """);
        }

        foreach (var property in bindAlsoProperties)
        {
            code.AppendMultiline(
                $"""
                {General.GeneratedCodeViewModelAttribute}
                private {Classes.ViewModelEvent.Global}<{property.Type}> __{property.GetFieldName(false)}ChangedEvent;
                """);
        }

        return code;
    }

    private static CodeWriter AppendProperties(this CodeWriter code, in ViewModelDataSpan data)
    {
        code.AppendLoop(data.Fields, field =>
        {
            var type = field.Type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
            var name = field.FieldName;
            var propertyName = field.PropertyName;
            
            var getAccessor = field.GetAccess;
            var setAccessor = field.SetAccess;
            var generalAccessor = GetGeneralAccessor(getAccessor, setAccessor);
            
            var generalAccessorName = GetAccess(generalAccessor);

            if (field.IsReadOnly)
            {
                code.AppendMultiline(
                    $"""
                     {General.GeneratedCodeViewModelAttribute}
                     {generalAccessorName}{type} {propertyName} => {name};
                     
                     """);
            }
            else
            {
                var getAccessorName = getAccessor == generalAccessor ? "" : GetAccess(getAccessor);
                var setAccessorName = setAccessor == generalAccessor ? "" : GetAccess(setAccessor);
                
                code.AppendMultiline(
                    $$"""
                      {{General.GeneratedCodeViewModelAttribute}}
                      {{generalAccessorName}}{{type}} {{propertyName}}
                      {
                          {{getAccessorName}}get => {{name}};
                          {{setAccessorName}}set => Set{{propertyName}}(value);
                      }

                      """);
            }
        });
        
        return code;
        
        string GetAccess(SyntaxKind syntaxKind) => syntaxKind switch
        {
            SyntaxKind.PrivateKeyword => "private ",
            SyntaxKind.ProtectedKeyword => "protected ",
            SyntaxKind.PublicKeyword => "public ",
            _ => ""
        };

        SyntaxKind GetGeneralAccessor(SyntaxKind getAccessor, SyntaxKind setAccessor)
        {
            if (setAccessor == getAccessor) return getAccessor;
            if (getAccessor == SyntaxKind.PrivateKeyword) return setAccessor;
            if (setAccessor == SyntaxKind.PrivateKeyword) return getAccessor;
            if (getAccessor == SyntaxKind.ProtectedKeyword) return getAccessor;
            if (setAccessor == SyntaxKind.ProtectedKeyword) return setAccessor;

            return SyntaxKind.PrivateKeyword;
        }
    }

    private static void AppendSetMethods(this CodeWriter code, in ViewModelDataSpan data)
    {
        var fieldCount = data.Fields.Length;
        
        code.AppendLoop(data.Fields, (i, field) =>
        {
            if (field.IsReadOnly) return;
            
            var type = field.Type;
            var name = field.FieldName;
            var propertyName = field.PropertyName;
            var changedEvent = field.ViewModelEventName;

            var changedMethod = $"On{propertyName}Changed";
            var changingMethod = $"On{propertyName}Changing";

            code.AppendMultiline(
                    $"""
                     {General.GeneratedCodeViewModelAttribute}
                     private void Set{propertyName}({type} value)
                     """)
                .BeginBlock()
                .AppendMultiline(
                    $"""
                    if ({Classes.EqualityComparer.Global}<{type}>.Default.Equals({name}, value)) return;
                    
                    {changingMethod}({name}, value);
                    {name} = value;
                    {changedMethod}(value);
                    {changedEvent}?.Invoke({name});
                    """)
                .AppendLoop(field.BindAlso, bindAlso =>
                {
                    code.AppendLine($"__{bindAlso.GetFieldName(false)}ChangedEvent?.Invoke({bindAlso.Name});");
                })
                .EndBlock()
                .AppendLine();
                
            code.AppendMultiline(
                $"""
                {General.GeneratedCodeViewModelAttribute}
                partial void {changingMethod}({type} oldValue, {type} newValue);

                {General.GeneratedCodeViewModelAttribute}
                partial void {changedMethod}({type} newValue);
                """);

            code.AppendLineIf(i + 1 < fieldCount);
        });
    }
}