#nullable enable
using System;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public sealed class RequiredTypes : IEnumerable<Type>
    {
        private readonly IReadOnlyCollection<Type> _types;

        public RequiredTypes(MonoBinderValidableFieldInfo field)
        {
            var requireAttributes = field.GetCustomAttributes<RequireBinderAttribute>(false);
            _types = requireAttributes.Select(attribute => attribute.Type).ToArray();
        }

        public bool IsBinderMatchRequiredType(IBinder binder)
        {
            if (_types.Count is 0) return true;
            
            var binderInterfaces = binder.GetType().GetInterfaces();
            
            return binderInterfaces.Any(@interface =>
            {
                if (@interface == typeof(IAnyBinder)) return true;
                if (!@interface.IsGenericType) return false;
                
                if (@interface.GetGenericTypeDefinition() != typeof(IBinder<>) 
                    && @interface.GetGenericTypeDefinition() != typeof(IReverseBinder<>)) return false;
                
                return _types.Any(requiredType =>
                    @interface.GetGenericArguments()[0].IsAssignableFrom(requiredType));
            });
        }

        public IEnumerator<Type> GetEnumerator() =>
            _types.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            _types.GetEnumerator();
    }
}