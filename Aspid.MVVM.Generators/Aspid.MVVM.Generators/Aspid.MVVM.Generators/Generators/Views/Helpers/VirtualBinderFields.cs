using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using Aspid.Generators.Helper;
using Aspid.Generators.Helper.Unity;
using Aspid.MVVM.Generators.Generators.Ids.Data;
using Aspid.MVVM.Generators.Generators.Views.Data;
using Aspid.MVVM.Generators.Generators.ViewModels.Factories;
using Aspid.MVVM.Generators.Generators.ViewModels.Data.Infos;
using Classes = Aspid.MVVM.Generators.Generators.Descriptions.Classes;
using SymbolExtensions = Aspid.MVVM.Generators.Helpers.SymbolExtensions;

namespace Aspid.MVVM.Generators.Generators.Views.Helpers;

public static class VirtualBinderFields
{
    public static Dictionary<string, Info> Collect(in ViewDataSpan data)
    {
        var result = new Dictionary<string, Info>();
        if (data.GenericViews.Length == 0) return result;
        if (!IsAutoBinderFieldsEnabled(data.Symbol)) return result;
        if (InheritsFromScriptableObject(data.Symbol)) return result;

        var declared = new HashSet<string>();
        foreach (var member in data.Members)
            declared.Add(member.Id.SourceValue);
        foreach (var id in data.InheritedDeclaredIds)
            declared.Add(id);

        foreach (var genericView in data.GenericViews)
        {
            if (genericView.Type.TypeKind is TypeKind.Interface) continue;

            for (var viewModelType = genericView.Type; viewModelType is not null; viewModelType = viewModelType.BaseType)
            {
                CollectFromType(viewModelType, data, declared, result);
            }
        }

        return result;
    }

    private static void CollectFromType(
        ITypeSymbol viewModelType,
        in ViewDataSpan data,
        HashSet<string> declared,
        Dictionary<string, Info> result)
    {
        var bindableMembers = BindableMembersFactory.Create(viewModelType, data.Declaration, out _);
        if (bindableMembers.Length == 0) return;

        var membersBySymbol = new Dictionary<ISymbol, List<IBindableMemberInfo>>(SymbolEqualityComparer.Default);
        foreach (var bindable in bindableMembers)
        {
            if (!membersBySymbol.TryGetValue(bindable.Member, out var list))
            {
                list = [];
                membersBySymbol[bindable.Member] = list;
            }
            
            list.Add(bindable);
        }

        string? activeGroup = null;

        foreach (var symbol in viewModelType.GetMembers())
        {
            var groupStart = GetAttributeStringArgument(symbol, Classes.HeaderGroupStartAttribute);
            var groupName = GetAttributeStringArgument(symbol, Classes.HeaderGroupAttribute);
            var groupEnd = HasAttribute(symbol, Classes.HeaderGroupEndAttribute);

            if (groupStart is not null) activeGroup = groupStart;
            else if (groupName is not null) activeGroup = groupName;

            if (membersBySymbol.TryGetValue(symbol, out var bindablesForSymbol))
            {
                foreach (var bindableMember in bindablesForSymbol)
                {
                    var sourceValue = bindableMember.Id.SourceValue;
                    if (declared.Contains(sourceValue)) continue;

                    if (!result.TryGetValue(sourceValue, out var info))
                    {
                        info = new Info(bindableMember.Id, SymbolExtensions.GetFieldName(sourceValue, "_"));
                        result[sourceValue] = info;
                    }

                    var requireType = StripNullable(bindableMember.Type);
                    if (!info.RequireBinderTypes.Contains(requireType))
                        info.RequireBinderTypes.Add(requireType);

                    if (bindableMember is not BindableCommandInfo)
                        info.Header ??= GetAttributeStringArgument(symbol, Classes.HeaderAttribute);

                    if (activeGroup is not null)
                        info.HeaderGroup ??= activeGroup;

                    if (groupEnd && !info.HeaderGroupEnd)
                        info.HeaderGroupEnd = true;
                }
            }

            if (groupEnd) activeGroup = null;
        }
    }

    private static bool IsAutoBinderFieldsEnabled(INamedTypeSymbol viewSymbol)
    {
        foreach (var attribute in viewSymbol.GetAttributes())
        {
            if (attribute.AttributeClass?.ToDisplayString() != Classes.ViewAttribute.FullName) continue;

            foreach (var named in attribute.NamedArguments)
            {
                if (named.Key != "AutoBinderFields") continue;
                if (named.Value.Value is bool enabled) return enabled;
            }

            return true;
        }

        return true;
    }

    private static bool InheritsFromScriptableObject(INamedTypeSymbol viewSymbol)
    {
        var scriptableObjectFullName = UnityClasses.ScriptableObject.FullName;

        for (var type = viewSymbol.BaseType; type is not null; type = type.BaseType)
        {
            if (type.ToDisplayString() == scriptableObjectFullName) return true;
        }

        return false;
    }

    private static string? GetAttributeStringArgument(ISymbol symbol, AttributeText attributeText)
    {
        if (symbol.TryGetAnyAttributeInSelf(out var attribute, attributeText))
        {
            if (attribute.ConstructorArguments.Length is 0) return null;
            
            var value = attribute.ConstructorArguments[0].Value as string;
            if (!string.IsNullOrWhiteSpace(value)) return value;
        }

        return null;
    }

    private static bool HasAttribute(ISymbol symbol, string attributeFullName)
    {
        foreach (var attribute in symbol.GetAttributes())
        {
            if (attribute.AttributeClass?.ToDisplayString() == attributeFullName) 
                return true;
        }

        return false;
    }

    private static string StripNullable(string type) =>
        type.EndsWith("?") ? type.Substring(0, type.Length - 1) : type;
    
    public sealed class Info(IdData id, string fieldName)
    {
        public IdData Id { get; } = id;

        public string FieldName { get; } = fieldName;

        public List<string> RequireBinderTypes { get; } = [];

        public string? Header { get; set; }
        
        public string? HeaderGroup { get; set; }
        
        public bool HeaderGroupEnd { get; set; }
    }
}
