using System.Linq;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using MVVMGenerators.Helpers.Data;
using System.Collections.Immutable;
using MVVMGenerators.Helpers.Descriptions;
using MVVMGenerators.Generators.ViewModels.Data;
using MVVMGenerators.Helpers.Extensions.Symbols;
using MVVMGenerators.Generators.ViewModels.Data.Members;

namespace MVVMGenerators.Generators.ViewModels.Factories;

public static class BindableMembersFactory
{
    public static ImmutableArray<BindableMember> Create(ITypeSymbol symbol)
    {
        var members = new MembersByGroup(symbol);

        if (symbol.TypeKind is not TypeKind.Interface)
        {
            var bindableBindAlso = BindableBindAlsoFactory.Create(members.All);
            var bindableFields = BindableFieldFactory.Create(members.Fields, bindableBindAlso);
        
            var generatedProperties = bindableFields
                .Where(field => field.Type.ToString() == "bool")
                .Select(field => field.GeneratedName)
                .ToImmutableArray();
        
            var bindableCommands = BindableCommandFactory.Create(members.Methods, members.Properties, generatedProperties);
            var bindableMembers = new List<BindableMember>(bindableBindAlso.Count + bindableFields.Count + bindableCommands.Count);
        
            bindableMembers.AddRange(bindableFields);
            bindableMembers.AddRange(bindableCommands);
            bindableMembers.AddRange(bindableBindAlso);
            
            return bindableMembers.ToImmutableArray();
        }
        else
        {
            var bindableMembers = new List<BindableMember>();
            
            AddMembers(symbol);
            
            foreach (var @interface in symbol.AllInterfaces)
                AddMembers(@interface);

            return bindableMembers.ToImmutableArray();
            
            void AddMembers(ITypeSymbol @interface)
            {
                foreach (var property in @interface.GetMembers()
                             .OfType<IPropertySymbol>()
                             .Where(p => p.Type.ToDisplayStringGlobal() == Classes.IBindableMemberEventAdder.Global))
                {
                    bindableMembers.Add(new BindableMember(property, BindMode.None, property.Type.ToDisplayStringGlobal(), property.Name, property.Name));
                }
            }
        }
    }
}