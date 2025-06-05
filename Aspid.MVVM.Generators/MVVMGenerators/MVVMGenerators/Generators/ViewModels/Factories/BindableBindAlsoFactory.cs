using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Collections.Immutable;
using MVVMGenerators.Generators.ViewModels.Data.Members;
using MVVMGenerators.Helpers.Descriptions;
using MVVMGenerators.Helpers.Extensions.Symbols;
using static MVVMGenerators.Helpers.Descriptions.Classes;

namespace MVVMGenerators.Generators.ViewModels.Factories;

public static class BindableBindAlsoFactory
{
    public static IReadOnlyCollection<BindableBindAlso> Create(ImmutableArray<ISymbol> members)
    {
        var set = new HashSet<string>();
        var bindableBindAlso = new List<BindableBindAlso>();

        foreach (var member in members)
        {
            if (!member.HasAnyAttribute(BindAttribute, OneWayBindAttribute, TwoWayBindAttribute)) continue;
            if (!member.HasAnyAttribute(out var attribute, BindAlsoAttribute)) continue;
            
            var value = attribute!.ConstructorArguments[0].Value;
            if (value is null) continue;
                    
            set.Add(value.ToString());
        }

        foreach (var member in members)
        {
            if (set.Contains(member.Name))
                bindableBindAlso.Add(new BindableBindAlso(member));
        }

        return bindableBindAlso;
    }
}