using System;
using System.Linq;
using System.Reflection;
using System.CodeDom.Compiler;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal static class RelayCommandTypeExtensions
    {
        private const BindingFlags BindingAttr = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
        
        public static bool IsRelayCommandType(this Type type)
        {
            if (typeof(IRelayCommand).IsAssignableFrom(type)) return true;
                        
            var interfaces = new HashSet<Type>(collection: type.GetInterfaces());
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

        public static MethodInfo FindCommandMethodByName(this object target, FieldInfo generatedField)
        {
            var name = generatedField.GetGeneratedPropertyName();

            return target
                .GetType()
                .GetMembersInfosIncludingBaseClasses(bindingFlags: BindingAttr)
                .OfType<MethodInfo>()
                .Where(method => method.IsDefined(typeof(RelayCommandAttribute)))
                .FirstOrDefault(method => $"{method.Name}Command" == name);
        }
        
        public static PropertyInfo FindRelayCommandGeneratedProperty(this object target, MemberInfo generatedField) => generatedField is not FieldInfo fieldInfo 
            ? null 
            : FindRelayCommandGeneratedProperty(target, fieldInfo);

        public static PropertyInfo FindRelayCommandGeneratedProperty(this object target, FieldInfo generatedField)
        {
            if (!generatedField.IsDefined(typeof(GeneratedCodeAttribute))) return null;
            
            var propertyName = generatedField.GetGeneratedPropertyName();
            
            return target.GetType()
                .GetPropertyInfosIncludingBaseClasses(BindingAttr)
                .FirstOrDefault(property => property.Name == propertyName);
        }
    }
}