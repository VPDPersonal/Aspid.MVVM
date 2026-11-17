using System;
using System.Reflection;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Provides extension methods for reflected fields and properties.
    /// </summary>
    public static class MemberInfoExtensions
    {
        /// <summary>
        /// Returns the declared field or property type.
        /// </summary>
        /// <param name="member">The field or property to inspect.</param>
        /// <returns>The declared value type.</returns>
        /// <exception cref="ArgumentException"><paramref name="member"/> is not a field or property.</exception>
        public static Type GetMemberType(this MemberInfo member) => member switch
        {
            FieldInfo fieldInfo => fieldInfo.FieldType,
            PropertyInfo propertyInfo => propertyInfo.PropertyType,
            _ => throw new ArgumentException("Can't get the type of a " + member.GetType().Name)
        };
        
        /// <summary>
        /// Determines whether a field is read-only or a property has no setter.
        /// </summary>
        /// <param name="member">The field or property to inspect.</param>
        /// <returns><see langword="true"/> if the field is read-only or the property has no setter; otherwise, <see langword="false"/>.</returns>
        /// <exception cref="ArgumentException"><paramref name="member"/> is not a field or property.</exception>
        public static bool IsReadonly(this MemberInfo member) => member switch
        {
            FieldInfo fieldInfo => fieldInfo.IsInitOnly,
            PropertyInfo propertyInfo => !propertyInfo.CanWrite,
            _ => throw new ArgumentException("Can't check readonly status of a " + member.GetType().Name)
        };
        
        /// <summary>
        /// Writes a field or invokes a property setter.
        /// </summary>
        /// <param name="member">The field or writable property.</param>
        /// <param name="obj">The object containing the member.</param>
        /// <param name="value">The value to assign.</param>
        /// <exception cref="ArgumentException"><paramref name="member"/> is not a field or property with a setter.</exception>
        public static void SetValue(this MemberInfo member, object obj, object value)
        {
            switch (member)
            {
                case FieldInfo fieldInfo:
                    fieldInfo.SetValue(obj, value);
                    return;
                
                case PropertyInfo propertyInfo:
                    {
                        var method = propertyInfo.GetSetMethod(nonPublic: true);
                        if (method is null) throw new ArgumentException($"Property {member.Name} has no setter");
                
                        method.Invoke(obj, new[]{ value });
                        return;
                    }
                
                default: throw new ArgumentException("Can't set the value of a " + member.GetType().Name);
            }
        }
        
        /// <summary>
        /// Reads a field or invokes a property getter.
        /// </summary>
        /// <param name="member">The field or readable property.</param>
        /// <param name="obj">The object containing the member.</param>
        /// <returns>The member value.</returns>
        /// <exception cref="ArgumentException"><paramref name="member"/> is not a field or property.</exception>
        public static object GetValue(this MemberInfo member, object obj) => member switch
        {
            FieldInfo fieldInfo => fieldInfo.GetValue(obj),
            PropertyInfo propertyInfo => propertyInfo.GetGetMethod(true).Invoke(obj, null),
            _ => throw new ArgumentException("Can't get the value of a " + member.GetType().Name)
        };
    }
}