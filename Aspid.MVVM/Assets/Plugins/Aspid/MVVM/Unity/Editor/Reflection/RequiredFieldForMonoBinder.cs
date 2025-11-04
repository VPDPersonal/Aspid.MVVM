#nullable enable
using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    // TODO Aspid.MVVM – Write summary
    public sealed partial class RequiredFieldForMonoBinder : FieldInfo
    {
        // TODO Aspid.MVVM – Write summary
        public readonly string Id;

        // TODO Aspid.MVVM – Write summary
        public readonly object FieldContainerObj;

        // TODO Aspid.MVVM – Write summary
        public readonly Type FieldContainerObjType;

        // TODO Aspid.MVVM – Write summary
        public readonly IReadOnlyCollection<RequiredFieldForMonoBinder> Children;

        private bool? _isValidation;
        private readonly FieldInfo _field;
        private readonly IReadOnlyCollection<Type> _requiredTypes;

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

                // If Custom id is null then GetIdFromFieldName.
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
                BinderFieldInfoExtensions.GetBinderId(field.Name);

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
                    .ToArray();
                var types = new Type[assemblyQualifiedNames.Length];

                for (var i = 0; i < types.Length; i++)
                {
                    string assemblyQualifiedName;
                    var directType = FieldContainerObjType.IsArray ? FieldContainerObjType.GetElementType() :
                        FieldContainerObjType;
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

                    var type = Type.GetType(assemblyQualifiedName);
                    types[i] = type ?? throw new Exception($"Type {assemblyQualifiedName} not found");
                }

                return types;
            }
        }

        // TODO Aspid.MVVM – Write summary
        public bool IsBinderMatchRequiredType(IBinder binder)
        {
            if (_requiredTypes.Count is 0) return true;

            var interfaces = binder.GetType().GetInterfaces();

            return interfaces.Any(i =>
            {
                if (i == typeof(IAnyBinder)) return true;
                if (i == typeof(IAnyReverseBinder)) return true;

                if (!i.IsGenericType) return false;

                var definition = i.GetGenericTypeDefinition();
                if (definition != typeof(IBinder<>) && definition != typeof(IReverseBinder<>)) return false;

                var genericArguments = i.GetGenericArguments()[0];
                return _requiredTypes.Any(requiredType => genericArguments.IsAssignableFrom(requiredType));
            });
        }

        public bool IsValidation() =>
            IsValidation(this);

        // TODO Aspid.MVVM – Write summary
        public static bool IsValidation(RequiredFieldForMonoBinder field) =>
            field._isValidation ??= IsValidation(field._field);

        private static bool IsValidation(FieldInfo field)
        {
            var fieldType = field.FieldType;

            while (fieldType.IsGenericType)
            {
                if (fieldType.GetGenericTypeDefinition() == typeof(List<>))
                {
                    fieldType = field.FieldType;
                }
            }

            return !fieldType.IsArray
                ? typeof(IMonoBinderValidable).IsAssignableFrom(fieldType)
                : typeof(IMonoBinderValidable[]).IsAssignableFrom(fieldType);
        }

        // IMonoBinderValidable
        // IMonoBinderValidable[]
        // List<IMonoBinderValidable>
        // List<IMonoBinderValidable[]>
        // TODO Aspid.MVVM – Write summary
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