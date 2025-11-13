using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    // TODO Aspid.MVVM Unity – Write summary
    public class ViewModelMeta
    {
        public readonly Type Type;
        public readonly IReadOnlyList<BindablePropertyMeta> BindableProperties;
        
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

            const BindingFlags bindingAttr = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

            if (type.IsInterface)
            {
                BindableProperties = type.GetPropertyInfosIncludingBaseClasses(bindingAttr)
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