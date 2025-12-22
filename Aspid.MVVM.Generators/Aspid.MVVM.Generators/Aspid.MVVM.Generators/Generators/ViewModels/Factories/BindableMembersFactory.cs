using System.Linq;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Collections.Immutable;
using Aspid.MVVM.Generators.Helpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Aspid.MVVM.Generators.Generators.ViewModels.Data.Infos;

namespace Aspid.MVVM.Generators.Generators.ViewModels.Factories;

public static class BindableMembersFactory
{
    public static ImmutableArray<IBindableMemberInfo> Create(ITypeSymbol symbol, TypeDeclarationSyntax declaration)
    {
        var members = new MembersByGroup(symbol);

        var bindableFields = BindableFieldFactory.Create(members.Fields);
        var bindableBindAlso = BindableBindAlsoFactory.Create(members.All);
        var bindableProperties = BindablePropertyFactory.Create(declaration, members.Properties);
        
        var generatedProperties = bindableFields
            .Where(field => field.Type.ToString() == "bool")
            .Select(field => field.Name)
            .Concat(bindableProperties
                .Where(property => property.Type.ToString() == "bool")
                .Select(property => property.Name))
            .ToImmutableArray();
        
        var bindableCommands = BindableCommandFactory.Create(members.Methods, members.Properties, generatedProperties);
        
        var filteredBindableBindAlso = bindableBindAlso.Where(b => !b.HasBindAttribute).ToList();
        var bindableMembers = new List<IBindableMemberInfo>(filteredBindableBindAlso.Count + bindableFields.Count + bindableProperties.Count + bindableCommands.Count);
        
        bindableMembers.AddRange(bindableFields);
        bindableMembers.AddRange(bindableProperties);
        bindableMembers.AddRange(bindableCommands);
        bindableMembers.AddRange(filteredBindableBindAlso);
            
        return bindableMembers.ToImmutableArray();
    }
}