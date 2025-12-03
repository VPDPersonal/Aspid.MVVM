using System.Linq;
using Microsoft.CodeAnalysis;
using Aspid.Generators.Helper;
using System.Collections.Generic;
using System.Collections.Immutable;
using Aspid.MVVM.Generators.Generators.ViewModels.Data.Infos;
using Aspid.MVVM.Generators.Generators.ViewModels.Extensions;
using Aspid.MVVM.Generators.Generators.ViewModels.Data.Members;
using static Aspid.MVVM.Generators.Generators.Descriptions.Classes;
using BindMode = Aspid.MVVM.Generators.Generators.ViewModels.Data.BindMode;

namespace Aspid.MVVM.Generators.Generators.ViewModels.Factories;

public static class BindableFieldFactory
{
    public static IReadOnlyCollection<BindableFieldInfo> Create(ImmutableArray<IFieldSymbol> fields, IReadOnlyCollection<BindableBindAlsoInfo> bindableBindAlsos)
    {
        var bindableFields = new List<BindableFieldInfo>();

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
            
            bindableFields.Add(new BindableFieldInfo(field, mode, GetBindableBindAlso(field, bindableBindAlsos)));
        }

        return bindableFields;
    }

    private static ImmutableArray<BindableBindAlsoInfo> GetBindableBindAlso(IFieldSymbol field, IReadOnlyCollection<BindableBindAlsoInfo> allBindableBindAlsos)
    {
        var set = new HashSet<string>();

        foreach (var attribute in field.GetAttributes())
        {
            if (attribute.AttributeClass != null &&
                attribute.AttributeClass.ToDisplayStringGlobal() == BindAlsoAttribute)
            {
                var value = attribute.ConstructorArguments[0].Value;
                if (value is null) continue;

                set.Add(value.ToString());
            }
        }

        return allBindableBindAlsos.Where(bindableBindAlso => 
            set.Contains(bindableBindAlso.Name)).ToImmutableArray();
    }
}