#nullable enable
using System;
using System.Linq;
using UnityEngine;
using System.Reflection;
using System.Collections;
using Aspid.MVVM.Validation;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// <see cref="FieldInfo"/> wrapper with binding requirements and nested binder fields.
    /// </summary>
    public sealed partial class RequiredFieldForMonoBinder : FieldInfo
    {
        /// <summary>
        /// The binding identifier, prefixed by parent identifiers for nested fields.
        /// </summary>
        public readonly string Id;
        /// <summary>
        /// The object containing the reflected field.
        /// </summary>
        public readonly object FieldContainerObj;
        /// <summary>
        /// The runtime type of the field container.
        /// </summary>
        public readonly Type FieldContainerObjType;
        /// <summary>
        /// The nested required binder fields.
        /// </summary>
        public readonly IReadOnlyCollection<RequiredFieldForMonoBinder> Children;

        private bool? _isValidation;
        private readonly FieldInfo _field;
        private readonly IReadOnlyCollection<Type> _requiredTypes;
        
        /// <summary>
        /// Gets the accepted value types; an empty collection permits any value type.
        /// </summary>
        public IReadOnlyCollection<Type> RequiredTypes => _requiredTypes;

        /// <summary>
        /// Creates a field wrapper and resolves its binding requirements.
        /// </summary>
        /// <param name="fieldContainerObj">The object containing the field.</param>
        /// <param name="field">The reflected field.</param>
        public RequiredFieldForMonoBinder(object fieldContainerObj, FieldInfo field)
            : this(fieldContainerObj, field, null) { }

        private RequiredFieldForMonoBinder(
            object fieldContainerObj,
            FieldInfo field,
            RequiredFieldForMonoBinder? parent = null)
        {
            _field = field;
            FieldContainerObj = fieldContainerObj;
            FieldContainerObjType = fieldContainerObj.GetType();

            const BindingFlags bindingAttr = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance |
                BindingFlags.DeclaredOnly;
            var attributes = field.GetCustomAttributes<RequireBinderAttribute>().ToArray();

            Id = parent is null ? GetId() : $"{parent.Id}.{GetId()}";
            _requiredTypes = GetRequiredTypes();

            Children = GetChildren();
            return;

            string GetId()
            {
                // Get Custom id.
                var id = attributes
                    .Select(attribute => attribute.Id)
                    .FirstOrDefault(name => name is not null);

                // If Custom id is null, then GetIdFromFieldName.
                if (string.IsNullOrWhiteSpace(id)) return GetIdFromFieldName();

                // Get members by id.
                var nameMemberInfos = FieldContainerObjType.GetMember(id, bindingAttr);

                // Get Custom id from member value or GetIdFromFieldName.
                return nameMemberInfos.FirstOrDefault() switch
                {
                    FieldInfo fieldInfo => fieldInfo.GetValue(FieldContainerObj) as string,
                    PropertyInfo propertyInfo => propertyInfo.GetValue(FieldContainerObj) as string,
                    _ => null
                } ?? id!;
            }

            string GetIdFromFieldName() =>
                BinderIdUtility.FromFieldName(field.Name);

            IReadOnlyCollection<RequiredFieldForMonoBinder> GetChildren()
            {
                if (IsValidation(field)) return Array.Empty<RequiredFieldForMonoBinder>();

                var value = field.GetValue(FieldContainerObj);
                if (value is null) return Array.Empty<RequiredFieldForMonoBinder>();

                var children = new List<RequiredFieldForMonoBinder>();
                var unitySerializableType = field.GetUnitySerializableType();

                foreach (var childField in unitySerializableType.GetFields(bindingAttr))
                {
                    if (!IsRequireBinderField(childField)) continue;

                    // TODO Aspid.MVVM – Fix
                    if (value is IEnumerable enumerable)
                    {
                        foreach (var item in enumerable)
                        {
                            children.Add(new RequiredFieldForMonoBinder(item, childField, this));
                        }
                    }
                    else children.Add(new RequiredFieldForMonoBinder(value, childField, this));
                }

                return children;
            }

            Type[] GetRequiredTypes()
            {
                var assemblyQualifiedNames = attributes
                    .SelectMany(attribute => attribute.AssemblyQualifiedNames ?? Array.Empty<string>())
                    .Where(assemblyQualifiedName => !string.IsNullOrWhiteSpace(assemblyQualifiedName))
                    .ToArray();
                
                var types = new List<Type>(assemblyQualifiedNames.Length);

                for (var i = 0; i < assemblyQualifiedNames.Length; i++)
                {
                    string assemblyQualifiedName;
                    var directType = FieldContainerObjType.IsArray ? FieldContainerObjType.GetElementType() : FieldContainerObjType;
                    var membersInfo = directType!.GetMember(assemblyQualifiedNames[i], bindingAttr);

                    if (membersInfo.Length is not 0)
                    {
                        if (membersInfo.Length > 1)
                        {
                            assemblyQualifiedName = assemblyQualifiedNames[i];
                        }
                        else
                        {
                            assemblyQualifiedName = membersInfo.FirstOrDefault() switch
                            {
                                FieldInfo fieldInfo => (string)fieldInfo.GetValue(FieldContainerObj),
                                PropertyInfo propertyInfo => (string)propertyInfo.GetValue(FieldContainerObj),
                                _ => throw new Exception( /*TODO Apid.MVVM - Write Exception*/)
                            };
                        }
                    }
                    else
                    {
                        assemblyQualifiedName = assemblyQualifiedNames[i];
                    }

                    var type = Type.GetType(assemblyQualifiedName, false);
                    if (type is not null) types.Add(type);
                }

                return types.ToArray();
            }
        }
        
        /// <summary>
        /// Checks whether a binder supports an accepted value type.
        /// </summary>
        /// <param name="binder">The binder to inspect.</param>
        /// <returns><see langword="true"/> if any required type matches or no types are required; otherwise, <see langword="false"/>.</returns>
        public bool IsBinderMatchRequiredType(IBinder binder)
        {
            if (_requiredTypes.Count is 0) return true;

            return _requiredTypes.Any(type => BinderCompatibility.Matches(binder, type));
        }

        /// <summary>
        /// Determines whether this field directly holds validatable binders.
        /// </summary>
        /// <returns><see langword="true"/> if the field holds binders rather than a nested container; otherwise, <see langword="false"/>.</returns>
        public bool IsValidation() =>
            IsValidation(this);
        
        /// <summary>
        /// Determines whether a field directly holds validatable binders.
        /// </summary>
        /// <param name="field">The field wrapper to inspect.</param>
        /// <returns><see langword="true"/> if the field holds binders rather than a nested container; otherwise, <see langword="false"/>.</returns>
        public static bool IsValidation(RequiredFieldForMonoBinder field) =>
            field._isValidation ??= IsValidation(field._field);

        private static bool IsValidation(FieldInfo field)
        {
            var fieldType = field.FieldType;

            while (fieldType.IsGenericType && fieldType.GetGenericTypeDefinition() == typeof(List<>))
            {
                fieldType = fieldType.GetGenericArguments()[0];
            }

            return !fieldType.IsArray
                ? typeof(IMonoBinderValidatable).IsAssignableFrom(fieldType)
                : typeof(IMonoBinderValidatable[]).IsAssignableFrom(fieldType);
        }

        /// <summary>
        /// Determines whether a serialized field requires binder validation.
        /// </summary>
        /// <param name="field">The field to inspect; <see langword="null"/> never qualifies.</param>
        /// <returns><see langword="true"/> if the field has binding requirements or holds validatable binders; otherwise, <see langword="false"/>.</returns>
        public static bool IsRequireBinderField(FieldInfo? field)
        {
            if (field is null) return false;

            // Only Serializable fields.
            if (!field.IsPublic && !field.IsDefined(typeof(SerializeField))) return false;
            if (field.IsDefined(typeof(RequireBinderAttribute))) return true;

            return IsValidation(field);
        }
    }
}