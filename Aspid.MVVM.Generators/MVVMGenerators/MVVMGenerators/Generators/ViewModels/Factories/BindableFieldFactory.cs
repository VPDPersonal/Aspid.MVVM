using System.Linq;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Collections.Immutable;
using MVVMGenerators.Helpers.Descriptions;
using MVVMGenerators.Generators.ViewModels.Data;
using MVVMGenerators.Helpers.Extensions.Symbols;
using MVVMGenerators.Generators.ViewModels.Extensions;
using MVVMGenerators.Generators.ViewModels.Data.Members;

namespace MVVMGenerators.Generators.ViewModels.Factories;

public static class BindableFieldFactory
{
    public static IReadOnlyCollection<BindableField> Create(ImmutableArray<IFieldSymbol> fields, IReadOnlyCollection<BindableBindAlso> bindableBindAlsos)
    {
        var bindableFields = new List<BindableField>();

        foreach (var field in fields)
        {
            var mode = field.GetBindMode();

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
}