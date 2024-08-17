using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp;
using MVVMGenerators.Helpers.Descriptions;
using MVVMGenerators.Helpers.Extensions.Writer;

namespace MVVMGenerators.Generators.ViewModels.Body;

public static class PropertiesBody
{
    public static CodeWriter AppendPropertiesBody(this CodeWriter code, ViewModelDataSpan data)
    {
        code.AppendEvents(data)
            .AppendProperties(data)
            .AppendSetMethods(data);
        
        return code;
    }

    private static CodeWriter AppendEvents(this CodeWriter code, in ViewModelDataSpan data)
    {
        HashSet<IPropertySymbol> changedEvents = [];
        
        code.AppendLoop(data.Fields, field =>
        {
            if (field.IsReadOnly) return;

            foreach (var bindAlso in field.BindAlso)
                changedEvents.Add(bindAlso);
            
            Append(field.Type, field.PropertyName);
        });

        code.AppendLoop(changedEvents, changedEvent =>
        {
            Append(changedEvent.Type, changedEvent.Name);
        });

        return code;

        void Append(ITypeSymbol type, string propertyName)
        {
            code.AppendMultiline(
                $"""
                {General.GeneratedCodeViewModelAttribute}
                public event {Classes.Action.Global}<{type}> {propertyName}Changed;
                
                """);
        }
    }

    private static CodeWriter AppendProperties(this CodeWriter code, in ViewModelDataSpan data)
    {
        code.AppendLoop(data.Fields, field =>
        {
            var type = field.Type;
            var name = field.Name;
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
            var name = field.Name;
            var propertyName = field.PropertyName;

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
                    {changingMethod}({name}, value);
                    {name} = value;
                    {changedMethod}(value);
                    {propertyName}Changed?.Invoke({name});
                    """)
                .AppendLoop(field.BindAlso, bindAlso =>
                {
                    code.AppendLine($"{bindAlso.Name}Changed?.Invoke({bindAlso.Name});");
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