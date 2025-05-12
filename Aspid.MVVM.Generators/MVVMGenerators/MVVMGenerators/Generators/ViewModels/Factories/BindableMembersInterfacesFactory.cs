using System.Linq;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Collections.Immutable;
using MVVMGenerators.Helpers.Descriptions;
using MVVMGenerators.Generators.ViewModels.Data;
using MVVMGenerators.Helpers.Extensions.Symbols;

namespace MVVMGenerators.Generators.ViewModels.Factories;

public static class BindableMembersInterfacesFactory
{
    public static BindableMembersInterfacesData Create(INamedTypeSymbol symbol)
    {
        var interfaces = new List<BindableMembersInterfaceData>();
        var iBindableMembers = symbol.GetMembers().FirstOrDefault(m => m is INamedTypeSymbol { Name: "IBindableMembers" }) as INamedTypeSymbol;
        if (iBindableMembers is null) return new BindableMembersInterfacesData(ImmutableArray<BindableMembersInterfaceData>.Empty);
        
        foreach (var @interface in iBindableMembers.AllInterfaces
                     .Where(i => i.HasInterface(Classes.IViewModel)))
        {
            var names = new HashSet<string>();
            
            foreach (var property in @interface.GetMembers()
                         .OfType<IPropertySymbol>()
                         .Where(p => p.Type.ToDisplayStringGlobal() == Classes.IBindableMemberEventAdder.Global))
            {
                names.Add(property.Name);
            }
                
            interfaces.Add(new BindableMembersInterfaceData(@interface, names));
        }
        
        return new BindableMembersInterfacesData(interfaces.ToImmutableArray());
    }
}