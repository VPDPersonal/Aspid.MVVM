using System.Linq;
using Microsoft.CodeAnalysis;
using Aspid.Generator.Helpers;
using System.Collections.Generic;
using Aspid.MVVM.Generators.Ids.Extensions;
using Aspid.MVVM.Generators.ViewModels.Data.Members;
using static Aspid.MVVM.Generators.Descriptions.Classes;

namespace Aspid.MVVM.Generators.ViewModels.Factories;

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