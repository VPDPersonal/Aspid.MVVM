using System;
using System.Linq;
using System.Reflection;
using Aspid.FastTools.Reflection;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Metadata describing a single bindable property on an <see cref="IViewModel"/> type,
    /// including its resolved type, binding ID, generated property name, supported <see cref="BindMode"/>,
    /// and inspector layout hints (header text and vertical spacing read from Unity's
    /// <see cref="UnityEngine.HeaderAttribute"/> and <see cref="UnityEngine.SpaceAttribute"/>).
    /// </summary>
    public class BindablePropertyMeta
    {
        public readonly Type Type;
        public readonly string Id;
        public readonly string Name;
        public readonly BindMode Mode;
        public readonly string Header;
        public readonly bool IsCommand;

        public BindablePropertyMeta(Type memberContainerType, MemberInfo memberInfo)
        {
            if (!IsBindableProperty(memberContainerType, memberInfo))
                throw new ArgumentException("Invalid bindable property");

            Name = memberInfo.Name;

            var unityHeader = memberInfo.GetCustomAttribute<UnityEngine.HeaderAttribute>();
            Header = string.IsNullOrWhiteSpace(unityHeader?.header) ? null : unityHeader.header;
            
            var bindIdAttribute = memberInfo.GetCustomAttribute<BindIdAttribute>();
            Id = bindIdAttribute is not null ? bindIdAttribute.Id : BinderFieldInfoExtensions.GetBinderId(memberInfo.Name);
            
            if (memberContainerType.IsInterface)
            {
                Mode = BindMode.TwoWay;
                var propertyInfo = (PropertyInfo)memberInfo;

                Type = propertyInfo.PropertyType == typeof(IBinderAdder)
                    ? null 
                    : propertyInfo.PropertyType.GetGenericArguments()[0];
                
                return;
            }
            
            switch (memberInfo)
            {
                case FieldInfo fieldInfo:
                    Type =  fieldInfo.FieldType;
                    
                    Mode = fieldInfo.GetCustomAttribute<BaseBindAttribute>() switch
                    {
                        OneWayBindAttribute => BindMode.OneWay,
                        TwoWayBindAttribute => BindMode.TwoWay,
                        OneTimeBindAttribute => BindMode.OneTime,
                        OneWayToSourceBindAttribute => BindMode.OneWayToSource,
                        _ => Mode
                    };
                    break;
                
                case PropertyInfo propertyInfo:
                    Mode = BindMode.OneWay;
                    Type =  propertyInfo.PropertyType;
                    break;
                
                case MethodInfo methodInfo:
                    if (bindIdAttribute is null)
                        Id += "Command";

                    IsCommand = true;
                    Mode = BindMode.OneTime;
                    Name += "Command";
                    
                    Type = memberContainerType.GetMembersInfosIncludingBaseClasses(bindingFlags: BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                        .OfType<PropertyInfo>()
                        .FirstOrDefault(property => property.Name == Name)?.PropertyType;
                    break;
            }
        }
        
        public static bool IsBindableProperty(Type memberContainerType, MemberInfo memberInfo)
        {
            if (memberInfo.IsDefined(typeof(IgnoreBindAttribute))) return false;
            
            if (memberContainerType.IsInterface)
            {
                if (memberInfo is not PropertyInfo propertyInfo) return false;
                if (!propertyInfo.CanRead) return false;

                var propertyType = propertyInfo.PropertyType;
                if (propertyType == typeof(IBinderAdder)) return true;
                
                if (!propertyType.IsGenericType) return false;
                
                if (propertyType.GetGenericTypeDefinition() == typeof(IReadOnlyValueBindableMember<>)) return true;
                if (propertyType.GetGenericTypeDefinition() == typeof(IReadOnlyBindableMember<>)) return true;
                if (propertyType.GetGenericTypeDefinition() == typeof(IBindableMember<>)) return true;

                return false;
            }
            
            switch (memberInfo)
            {
                case FieldInfo fieldInfo when fieldInfo.IsDefined(typeof(BaseBindAttribute)): return true;
                case PropertyInfo propertyInfo:
                    {
                        if (propertyInfo.IsDefined(typeof(BaseBindAttribute))) return true;
                        
                        return memberContainerType.GetMembers(bindingAttr: BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                            .Where(member => member.IsDefined(typeof(BindAlsoAttribute)))
                            .SelectMany(member => member
                                .GetCustomAttributes<BindAlsoAttribute>()
                                .Select(attribute => attribute.PropertyName))
                            .Any(propertyName => propertyInfo.Name == propertyName);
                    }
                case MethodInfo methodInfo: return methodInfo.IsDefined(typeof(RelayCommandAttribute));
                default: return false;
            }
        }
    }
}