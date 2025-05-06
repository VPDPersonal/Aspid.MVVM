using System.Linq;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Collections.Immutable;
using MVVMGenerators.Helpers.Descriptions;
using MVVMGenerators.Generators.ViewModels.Data;
using MVVMGenerators.Helpers.Extensions.Symbols;
using MVVMGenerators.Generators.ViewModels.Data.Members;

namespace MVVMGenerators.Generators.ViewModels.Factories;

public static class BindableFieldFactory
{
    public static IReadOnlyCollection<BindableField> Create(ImmutableArray<IFieldSymbol> fields, IReadOnlyCollection<BindableBindAlso> bindableBindAlsos)
    {
        var bindableFields = new List<BindableField>();

        foreach (var field in fields)
        {
            var mode = GetBindMode(field);

            switch (mode)
            {
                case BindMode.OneTime: break;

                case BindMode.OneWay:
                case BindMode.TwoWay:
                case BindMode.OneWayToSource:
                    {
                        if (field.IsReadOnly) continue;
                        break;
                    }
                
                default: continue;
            }
            
            bindableFields.Add(new BindableField(field, mode, GetBindableBindAlso(field, bindableBindAlsos)));
        }

        return bindableFields;
    }

    private static ImmutableArray<BindableBindAlso> GetBindableBindAlso(IFieldSymbol field, IReadOnlyCollection<BindableBindAlso> allBindableBindAlsos)
    {
        var set = new HashSet<string>();

        foreach (var attribute in field.GetAttributes())
        {
            if (attribute.AttributeClass != null &&
                attribute.AttributeClass.ToDisplayStringGlobal() == Classes.BindAlsoAttribute.Global)
            {
                var value = attribute.ConstructorArguments[0].Value;
                if (value is null) continue;

                set.Add(value.ToString());
            }
        }

        return allBindableBindAlsos.Where(bindableBindAlso => 
            set.Contains(bindableBindAlso.SourceName) || set.Contains(bindableBindAlso.GeneratedName)).ToImmutableArray();
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
        
        return (BindMode)(-1);
    }
}