using System;
using System.Linq;
using Aspid.FastTools;
using System.Reflection;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Reflected bindable member metadata for a ViewModel type.
    /// </summary>
    public class ViewModelMeta
    {
        /// <summary>
        /// The inspected ViewModel type.
        /// </summary>
        public readonly Type Type;
        /// <summary>
        /// The bindable members, including inherited members.
        /// </summary>
        public readonly IReadOnlyList<BindablePropertyMeta> BindableProperties;
        
        /// <summary>
        /// Collects binding metadata for a type.
        /// </summary>
        /// <param name="type">The type implementing <see cref="IViewModel"/>.</param>
        /// <exception cref="ArgumentException"><paramref name="type"/> does not implement <see cref="IViewModel"/>.</exception>
        public ViewModelMeta(Type type)
        {
            Type = type;

            if (type == typeof(IViewModel))
            {
                BindableProperties = Array.Empty<BindablePropertyMeta>();
                return;
            }
            
            if (type.GetInterfaces().All(i => i != typeof(IViewModel)))
                throw new ArgumentException($"{type} must implement {nameof(IViewModel)}");

            const BindingFlags bindingAttr = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;

            if (type.IsInterface)
            {
                BindableProperties = type.GetMembersInfosIncludingBaseClasses(bindingAttr)
                    .OfType<PropertyInfo>()
                    .Where(member => BindablePropertyMeta.IsBindableProperty(type, member))
                    .OrderBy(member => member, new MemberComparer())
                    .Select(member => new BindablePropertyMeta(type, member))
                    .ToArray();
            }
            else
            {
                BindableProperties = type.GetMembersInfosIncludingBaseClasses(bindingAttr)
                    .Where(member => BindablePropertyMeta.IsBindableProperty(type, member))
                    .OrderBy(member => member, new MemberComparer())
                    .Select(member => new BindablePropertyMeta(type, member))
                    .ToArray();
            }
        }
        
        private sealed class MemberComparer : IComparer<MemberInfo>
        {
            public int Compare(MemberInfo x, MemberInfo y)
            {
                if (x is MethodInfo)
                {
                    return y is MethodInfo ? 0 : -1;
                }
                
                if (y is MethodInfo)
                {
                    return -1;
                }

                return 0;
            }
        }
    }
}