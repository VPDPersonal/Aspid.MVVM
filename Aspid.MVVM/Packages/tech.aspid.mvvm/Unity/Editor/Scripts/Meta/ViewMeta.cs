using System;
using System.Linq;
using Aspid.FastTools;
using System.Reflection;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Reflected binder metadata for a View type.
    /// </summary>
    public class ViewMeta
    {
        /// <summary>
        /// The inspected View type.
        /// </summary>
        public readonly Type Type;
        /// <summary>
        /// The binder members, including inherited members.
        /// </summary>
        public readonly IReadOnlyList<BinderPropertyMeta> BinderProperties;
        
        /// <summary>
        /// Collects binding metadata for a type.
        /// </summary>
        /// <param name="type">The type implementing <see cref="IView"/>.</param>
        /// <exception cref="ArgumentException"><paramref name="type"/> does not implement <see cref="IView"/>.</exception>
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