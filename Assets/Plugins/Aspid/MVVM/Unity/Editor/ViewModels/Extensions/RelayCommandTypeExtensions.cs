using System;
using System.Linq;
using System.Collections.Generic;

namespace Aspid.MVVM.Unity
{
    internal static class RelayCommandTypeExtensions
    {
        public static bool IsRelayCommandType(this Type type)
        {
            if (typeof(IRelayCommand).IsAssignableFrom(type)) return true;
                        
            var interfaces = new HashSet<Type>(type.GetInterfaces());
            if (type.IsInterface) interfaces.Add(type);
                        
            return interfaces.Any(i =>
            { 
                if (!i.IsGenericType) return false;
                var genericTypeDefinition = i.GetGenericTypeDefinition();
                                
                return genericTypeDefinition == typeof(IRelayCommand<>)
                    || genericTypeDefinition == typeof(IRelayCommand<,>)
                    || genericTypeDefinition == typeof(IRelayCommand<,,>)
                    || genericTypeDefinition == typeof(IRelayCommand<,,,>);
            });
        }
    }
}