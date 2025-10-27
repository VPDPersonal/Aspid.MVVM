using Microsoft.CodeAnalysis;
using Aspid.Generators.Helper;
using System.Collections.Generic;
using System.Collections.Immutable;
using Aspid.MVVM.Generators.Generators.ViewModels.Data.Members;
using static Aspid.MVVM.Generators.Generators.Descriptions.Classes;

namespace Aspid.MVVM.Generators.Generators.ViewModels.Factories;

public static class BindableBindAlsoFactory
{
    public static IReadOnlyCollection<BindableBindAlso> Create(ImmutableArray<ISymbol> members)
    {
        var set = new HashSet<string>();
        var bindableBindAlso = new List<BindableBindAlso>();

        foreach (var member in members)
        {
            if (!member.HasAnyAttributeInSelf(BindAttribute, OneWayBindAttribute, TwoWayBindAttribute)) continue;
            if (!member.TryGetAnyAttributeInSelf(out var attribute, BindAlsoAttribute)) continue;
            
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