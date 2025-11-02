#nullable enable
using System;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    // TODO Aspid.MVVM – Add Comments
    public sealed class RequiredTypes : IEnumerable<Type>
    {
        private readonly IReadOnlyCollection<Type> _types;

        public RequiredTypes(object target, MonoBinderValidableFieldInfo field)
        {
            var requireAttributes = field.GetCustomAttributes<RequireBinderAttribute>(false).ToArray();
            
            var assemblyQualifiedNames = requireAttributes.SelectMany(attribute => attribute.AssemblyQualifiedNames).ToArray();
            var types = new Type[assemblyQualifiedNames.Length];

            for (var i = 0; i < types.Length; i++)
            {
                string assemblyQualifiedName;
                var membersInfo = target.GetType().GetMember(assemblyQualifiedNames[i], BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
               
                if (membersInfo.Length is not 0)
                {
                    if (membersInfo.Length > 1) throw new Exception(/*TODO Apid.MVVM - Write Exception*/);
                    
                    assemblyQualifiedName = membersInfo.First() switch
                    {
                        FieldInfo fieldInfo => (string)fieldInfo.GetValue(target),
                        PropertyInfo propertyInfo => (string)propertyInfo.GetValue(target),
                        _ => throw new Exception(/*TODO Apid.MVVM - Write Exception*/)
                    };
                }
                else
                {
                    assemblyQualifiedName  = assemblyQualifiedNames[i];
                }
                
                var type = Type.GetType(assemblyQualifiedName);
                types[i] = type ?? throw new Exception($"Type {assemblyQualifiedName} not found");
            }
            
            _types =  types;
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