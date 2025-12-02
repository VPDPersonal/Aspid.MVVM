using System.Linq;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Collections.Immutable;
using Aspid.MVVM.Generators.Helpers;
using Aspid.MVVM.Generators.Generators.ViewModels.Data.Infos;

namespace Aspid.MVVM.Generators.Generators.ViewModels.Factories;

public static class BindableMembersFactory
{
    public static ImmutableArray<IBindableMemberInfo> Create(ITypeSymbol symbol)
    {
        var members = new MembersByGroup(symbol);

        var bindableBindAlso = BindableBindAlsoFactory.Create(members.All);
        var bindableFields = BindableFieldFactory.Create(members.Fields, bindableBindAlso);
        
        var generatedProperties = bindableFields
            .Where(field => field.Type.ToString() == "bool")
            .Select(field => field.GeneratedName)
            .ToImmutableArray();
        
        var bindableCommands = BindableCommandFactory.Create(members.Methods, members.Properties, generatedProperties);
        var bindableMembers = new List<IBindableMemberInfo>(bindableBindAlso.Count + bindableFields.Count + bindableCommands.Count);
        
        bindableMembers.AddRange(bindableFields);
        bindableMembers.AddRange(bindableCommands);
        bindableMembers.AddRange(bindableBindAlso);
            
        return bindableMembers.ToImmutableArray();
    }
}