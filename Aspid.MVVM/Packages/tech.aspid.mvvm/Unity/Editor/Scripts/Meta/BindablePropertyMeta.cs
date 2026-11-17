using System;
using System.Linq;
using Aspid.FastTools;
using System.Reflection;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Binding and Inspector layout metadata for a ViewModel member.
    /// </summary>
    public class BindablePropertyMeta
    {
        /// <summary>
        /// The bound value type; <see langword="null"/> when it cannot be resolved.
        /// </summary>
        public readonly Type Type;
        /// <summary>
        /// The binding identifier.
        /// </summary>
        public readonly string Id;
        /// <summary>
        /// The property name, including the generated command suffix.
        /// </summary>
        public readonly string Name;
        /// <summary>
        /// The resolved binding mode.
        /// </summary>
        public readonly BindMode Mode;
        /// <summary>
        /// The plain Inspector header; <see langword="null"/> when absent.
        /// </summary>
        public readonly string Header;
        /// <summary>
        /// The foldout title for this member; <see langword="null"/> when absent.
        /// </summary>
        public readonly string HeaderGroup;
        /// <summary>
        /// The foldout range title; <see langword="null"/> when no range starts here.
        /// </summary>
        public readonly string HeaderGroupStart;
        /// <summary>
        /// Indicates whether this member ends the preceding foldout range.
        /// </summary>
        public readonly bool HeaderGroupEnd;

        /// <summary>
        /// Creates binding metadata for a ViewModel member.
        /// </summary>
        /// <param name="memberContainerType">The ViewModel type containing the member.</param>
        /// <param name="memberInfo">The member to inspect.</param>
        /// <exception cref="ArgumentException"><paramref name="memberInfo"/> is not a supported bindable member.</exception>
        public BindablePropertyMeta(Type memberContainerType, MemberInfo memberInfo)
        {
            if (!IsBindableProperty(memberContainerType, memberInfo))
                throw new ArgumentException("Invalid bindable property");

            Name = memberInfo.Name;

            var unityHeader = memberInfo.GetCustomAttribute<UnityEngine.HeaderAttribute>();
            Header = string.IsNullOrWhiteSpace(unityHeader?.header) ? null : unityHeader.header;

            var headerGroup = memberInfo.GetCustomAttribute<HeaderGroupAttribute>();
            HeaderGroup = string.IsNullOrWhiteSpace(headerGroup?.Title) ? null : headerGroup.Title;

            var headerGroupStart = memberInfo.GetCustomAttribute<HeaderGroupStartAttribute>();
            HeaderGroupStart = string.IsNullOrWhiteSpace(headerGroupStart?.Title) ? null : headerGroupStart.Title;

            HeaderGroupEnd = memberInfo.IsDefined(typeof(HeaderGroupEndAttribute));
            
            var bindIdAttribute = memberInfo.GetCustomAttribute<BindIdAttribute>();
            Id = bindIdAttribute is not null ? bindIdAttribute.Id : BinderIdUtility.FromFieldName(memberInfo.Name);
            
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
                    
                    Mode = ResolveMode(fieldInfo);
                    break;
                
                case PropertyInfo propertyInfo:
                    Mode = propertyInfo.IsDefined(typeof(BaseBindAttribute))
                        ? ResolveMode(propertyInfo)
                        : BindMode.OneWay;
                    Type =  propertyInfo.PropertyType;
                    break;
                
                case MethodInfo:
                    if (bindIdAttribute is null)
                        Id += "Command";
                    
                    Mode = BindMode.OneTime;
                    Name += "Command";
                    
                    Type = memberContainerType.GetMembersInfosIncludingBaseClasses(bindingFlags: BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                        .OfType<PropertyInfo>()
                        .FirstOrDefault(property => property.Name == Name)?.PropertyType;
                    break;
            }
        }
        
        /// <summary>
        /// Determines whether a member participates in ViewModel binding.
        /// </summary>
        /// <param name="memberContainerType">The ViewModel type containing the member.</param>
        /// <param name="memberInfo">The member to inspect.</param>
        /// <returns><see langword="true"/> if the member exposes a supported binding or relay command; otherwise, <see langword="false"/>.</returns>
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
                case FieldInfo fieldInfo when fieldInfo.IsDefined(typeof(BaseBindAttribute)):
                    return ResolveMode(fieldInfo) is not BindMode.None;
                case PropertyInfo propertyInfo:
                    {
                        if (propertyInfo.IsDefined(typeof(BaseBindAttribute)))
                            return ResolveMode(propertyInfo) is not BindMode.None;
                        
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

        private static BindMode ResolveMode(MemberInfo member)
        {
            var requested = member.GetCustomAttribute<BaseBindAttribute>() switch
            {
                BindAttribute bind => bind.Mode,
                OneWayBindAttribute => BindMode.OneWay,
                TwoWayBindAttribute => BindMode.TwoWay,
                OneTimeBindAttribute => BindMode.OneTime,
                OneWayToSourceBindAttribute => BindMode.OneWayToSource,
                _ => BindMode.None
            };

            switch (member)
            {
                case FieldInfo field:
                    {
                        var mode = requested ?? (field.IsInitOnly || field.IsLiteral ? BindMode.OneTime : BindMode.TwoWay);

                        return (field.IsInitOnly || field.IsLiteral) && mode is not BindMode.OneTime
                            ? BindMode.None
                            : mode;
                    }
                case PropertyInfo property:
                    {
                        var mode = requested ?? (property.GetMethod is null
                            ? BindMode.OneWayToSource
                            : property.SetMethod is null ? BindMode.OneWay : BindMode.TwoWay);

                        if (property.GetMethod is null && mode is not BindMode.OneWayToSource) return BindMode.None;
                        if (property.SetMethod is null && mode is not (BindMode.OneWay or BindMode.OneTime)) return BindMode.None;
                        return mode;
                    }

                default: return BindMode.None;
            }
        }
    }
}
