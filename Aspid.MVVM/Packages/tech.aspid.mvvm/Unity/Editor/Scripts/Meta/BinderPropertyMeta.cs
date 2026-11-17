using System;
using System.Reflection;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Binding metadata for a View member.
    /// </summary>
    public class BinderPropertyMeta
    {
        /// <summary>
        /// The binder type, or the declared collection type.
        /// </summary>
        public readonly Type Type;
        /// <summary>
        /// The binding identifier.
        /// </summary>
        public readonly string Id;
        /// <summary>
        /// The declared member name.
        /// </summary>
        public readonly string Name;
        
        /// <summary>
        /// Creates binding metadata for a View member.
        /// </summary>
        /// <param name="memberInfo">The member to inspect.</param>
        /// <exception cref="ArgumentException"><paramref name="memberInfo"/> is not a binder member.</exception>
        public BinderPropertyMeta(MemberInfo memberInfo)
        {
            if (!IsBinderProperty(memberInfo))
                throw new ArgumentException("Invalid bindable property");
            
            Name = memberInfo.Name;
            
            var asAttribute = memberInfo.GetCustomAttribute<AsBinderAttribute>();
            if (asAttribute is not null) Type = asAttribute.Type;
            else
            {
                Type = memberInfo switch
                {
                    FieldInfo fieldInfo => fieldInfo.FieldType,
                    PropertyInfo propertyInfo => propertyInfo.PropertyType,
                    _ => throw new ArgumentException("Invalid bindable property")
                };
            }
            
            var bindIdAttribute = memberInfo.GetCustomAttribute<BindIdAttribute>();
            Id = bindIdAttribute is not null ? bindIdAttribute.Id : BinderIdUtility.FromFieldName(memberInfo.Name);
        }

        /// <summary>
        /// Determines whether a member declares or wraps a binder.
        /// </summary>
        /// <param name="memberInfo">The member to inspect.</param>
        /// <returns><see langword="true"/> if the member supports binding and is not ignored; otherwise, <see langword="false"/>.</returns>
        public static bool IsBinderProperty(MemberInfo memberInfo)
        {
            if (memberInfo is MethodInfo) return false;
            if (memberInfo.IsDefined(typeof(IgnoreBindAttribute))) return false;
            if (memberInfo.IsDefined(typeof(AsBinderAttribute))) return true;

            var type = memberInfo switch
            {
                FieldInfo fieldInfo => fieldInfo.GetUnitySerializableType(),
                PropertyInfo propertyInfo => propertyInfo.GetUnitySerializableType(),
                _ => null
            };

            return type is not null && typeof(IBinder).IsAssignableFrom(type);
        }
    }
}