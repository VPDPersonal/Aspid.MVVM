using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    // TODO Aspid.MVVM Unity – Write summary
    public class ViewMeta
    {
        public readonly Type Type;
        public readonly IReadOnlyList<BinderPropertyMeta> BinderProperties;
        
        public ViewMeta(Type type)
        {
            Type = type;
            
            if (type.GetInterfaces().All(i => i != typeof(IView)))
                throw new ArgumentException($"{type} must implement {nameof(IView)}");

            const BindingFlags bindingAttr = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            BinderProperties = type.GetMembersInfosIncludingBaseClasses(bindingAttr)
                .Where(BinderPropertyMeta.IsBinderProperty)
                .Select(member => new BinderPropertyMeta(member))
                .ToArray();
        }
    }
}