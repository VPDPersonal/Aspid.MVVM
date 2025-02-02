using System;
using System.Linq;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Collections.Immutable;
using MVVMGenerators.Helpers.Descriptions;
using MVVMGenerators.Generators.ViewModels.Data.Members;
using MVVMGenerators.Generators.ViewModels.Data.Members.Fields;
using MVVMGenerators.Helpers.Extensions.Symbols;

namespace MVVMGenerators.Generators.ViewModels.Factories;

public readonly struct ViewModelFieldFactory(IReadOnlyCollection<IPropertySymbol> properties)
{
    public bool TryCreate(IFieldSymbol field, out ViewModelField viewModelField)
    {
        viewModelField = default;
        var mode = GetBindMode(field);
        
        switch (mode)
        {
            case BindMode.None: return false;
            case BindMode.OneWay:
                {
                    if (field.IsReadOnly) goto case BindMode.OneTime;
                    
                    viewModelField = new ViewModelField(BindMode.OneWay, field, GetBindAlso(field));
                    return true;
                }
                break;
            case BindMode.TwoWay:
                {
                    if (field.IsReadOnly) return false;
                    
                    viewModelField = new ViewModelField(BindMode.TwoWay, field, GetBindAlso(field));
                    return true;
                }
            case BindMode.OneTime:
                {
                    viewModelField = new ViewModelField(BindMode.OneTime, field, ImmutableArray<BindAlsoProperty>.Empty);
                    return true;
                }
            case BindMode.OneWayToSource:
                {
                    if (field.IsReadOnly) return false;

                    viewModelField = new ViewModelField(BindMode.OneWayToSource, field,
                        ImmutableArray<BindAlsoProperty>.Empty);
                    return true;
                }
            
            default: throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
        }
    }
    
    private ImmutableArray<BindAlsoProperty> GetBindAlso(IFieldSymbol field)
    {
        var bindAlso = new List<BindAlsoProperty>();
        var attributesArgument = new List<string?>();
            
        foreach (var attribute in field.GetAttributes())
        {
            if (attribute.ConstructorArguments.Length != 1) continue;
            if (attribute.AttributeClass?.ToDisplayString() != Classes.BindAlsoAttribute.FullName) continue;
            attributesArgument.Add(attribute.ConstructorArguments[0].Value?.ToString());
        }

        foreach (var property in properties)
        {
            if (attributesArgument.Any(argument => property.Name == argument))
                bindAlso.Add(new BindAlsoProperty(property));
        }
            
        return ImmutableArray.CreateRange(bindAlso);
    }
    
    private static BindMode GetBindMode(IFieldSymbol field)
    {
        if (field.HasAttribute(Classes.BindAttribute, out var bindAttribute))
        {
            if (bindAttribute!.ConstructorArguments.Length is 0)
                return field.IsReadOnly ? BindMode.OneTime : BindMode.TwoWay;

            return (BindMode)(int)bindAttribute!.ConstructorArguments[0].Value!;
        }
        
        if (field.HasAttribute(Classes.OneWayBindAttribute))
            return BindMode.OneWay;
        
        if (field.HasAttribute(Classes.TwoWayBindAttribute))
            return BindMode.TwoWay;
        
        if (field.HasAttribute(Classes.OneTimeBindAttribute))
            return BindMode.OneTime;
        
        if (field.HasAttribute(Classes.OneWayToSourceBindAttribute))
            return BindMode.OneWayToSource;

        return BindMode.None;
    }
}