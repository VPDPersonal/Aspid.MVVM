using System.Linq;
using Microsoft.CodeAnalysis;
using Aspid.Generators.Helper;
using System.Collections.Generic;
using Aspid.MVVM.Generators.Generators.Ids.Extensions;
using Aspid.MVVM.Generators.Generators.ViewModels.Data.Members;
using static Aspid.MVVM.Generators.Generators.Descriptions.Classes;

namespace Aspid.MVVM.Generators.Generators.ViewModels.Factories;

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
            if (!@interface.HasAnyInterfaceInSelfAndBases(IViewModel)) return;
            
            foreach (var property in @interface.GetMembers()
                         .OfType<IPropertySymbol>()
                         .Where(p =>
                         {
                             var type = p.Type.ToDisplayStringGlobal();
                             return type.Contains(IBinderAdder)
                                 || type.Contains(IReadOnlyBindableMember)
                                 || type.Contains(IReadOnlyValueBindableMember);
                         }))
            {
                if (property.HasAnyAttributeInSelf(IgnoreAttribute)) continue;

                var id = property.GetId();
                dictionary[id] = new CustomViewModelInterface(id, property, @interface);
            }
        }
    }
}