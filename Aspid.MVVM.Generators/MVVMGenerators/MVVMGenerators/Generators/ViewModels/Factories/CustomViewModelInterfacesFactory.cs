using System.Linq;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using MVVMGenerators.Generators.Ids.Extensions;
using MVVMGenerators.Helpers.Extensions.Symbols;
using MVVMGenerators.Generators.ViewModels.Data.Members;
using static MVVMGenerators.Helpers.Descriptions.Classes;

namespace MVVMGenerators.Generators.ViewModels.Factories;

public static class CustomViewModelInterfacesFactory
{
    public static Dictionary<string, CustomViewModelInterface> Create(ITypeSymbol symbol)
    {
        var dictionary = new Dictionary<string, CustomViewModelInterface>();
        AddMembers(symbol);
        
        foreach (var @interface in symbol.AllInterfaces)
            AddMembers(@interface);

        return dictionary;
        
        void AddMembers(ITypeSymbol @interface)
        {
            if (!@interface.HasInterfaceInSelfOrBases(IViewModel)) return;
            
            foreach (var property in @interface.GetMembers()
                         .OfType<IPropertySymbol>()
                         .Where(p => p.Type.ToDisplayStringGlobal() == IBindableMemberEventAdder))
            {
                if (property.HasAnyAttribute(IgnoreAttribute)) continue;

                var id = property.GetId();
                dictionary[id] = new CustomViewModelInterface(id, property.Name, @interface);
            }
        }
    }
}