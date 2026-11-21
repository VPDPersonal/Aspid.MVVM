using Microsoft.CodeAnalysis;
using Aspid.Generators.Helper;
using System.Collections.Generic;
using System.Collections.Immutable;
using Aspid.MVVM.Generators.Generators.ViewModels.Data.Infos;
using static Aspid.MVVM.Generators.Generators.Descriptions.Classes;

namespace Aspid.MVVM.Generators.Generators.ViewModels.Factories;

public static class BindableBindAlsoFactory
{
    public static IReadOnlyCollection<BindableBindAlsoInfo> Create(ImmutableArray<ISymbol> members)
    {
        var set = new HashSet<string>();
        var bindableBindAlso = new List<BindableBindAlsoInfo>();

        foreach (var member in members)
        {
            if (!member.HasAnyAttributeInSelf(BindAttribute, OneWayBindAttribute, TwoWayBindAttribute)) continue;

            foreach (var attribute in member.GetAttributesInSelf(BindAlsoAttribute))
            {
                var value = attribute.ConstructorArguments[0].Value;
                if (value is null) continue;

                set.Add(value.ToString());
            }
        }

        foreach (var member in members)
        {
            if (member is not IPropertySymbol propertySymbol) continue;
            
            if (set.Contains(member.Name))
                bindableBindAlso.Add(new BindableBindAlsoInfo(propertySymbol));
        }

        return bindableBindAlso;
    }
}