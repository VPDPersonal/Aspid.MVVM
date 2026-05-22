using System;
using System.Reflection;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Metadata describing a single binder field or property on an <see cref="IView"/> type,
    /// including its resolved <see cref="IBinder"/> type, binding ID, and member name.
    /// </summary>
    public class BinderPropertyMeta
    {
        public readonly Type Type;
        public readonly string Id;
        public readonly string Name;
        
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
            Id = bindIdAttribute is not null ? bindIdAttribute.Id : BinderFieldInfoExtensions.GetBinderId(memberInfo.Name);
        }

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