#nullable enable
using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using System.Reflection;
using UnityEditor.Search;
using UnityEngine.UIElements;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using Aspid.FastTools.UIElements;
using Aspid.FastTools.UIElements.Editors.Internal;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// <see cref="VisualElement"/> for serialized fields arranged in header groups.
    /// </summary>
    public sealed class AspidBaseInspectorVisualElement : VisualElement
    {
        /// <summary>
        /// Creates a grouped Inspector for serialized fields.
        /// </summary>
        /// <param name="serializedObject">The object to inspect.</param>
        /// <param name="title">The section title; <see langword="null"/> for none.</param>
        /// <param name="propertiesExcluding">The property names to skip; <see langword="null"/> skips none.</param>
        public AspidBaseInspectorVisualElement(
            SerializedObject serializedObject,
            string? title,
            IReadOnlyCollection<string>? propertiesExcluding = null)
        {
            var container = Build(serializedObject, title, propertiesExcluding);

            style.display = container.style.display;
            this.AddChild(container);
            
            RegisterCallback<GeometryChangedEvent>(_ =>
            {
                foreach (var field in container.Query<ObjectField>().Build())
                {
                    if (field?.value is not Component valueComponent) return;
                    if (field.childCount < 2) return;
                
                    var components = valueComponent.GetComponents(field.objectType);
                    if (components.Length < 2) return;
                
                    var index = 0;
                
                    foreach (var component in components)
                    {
                        index++;
                        if (valueComponent != component) continue;
                
                        var label = field[1].Q<Label>();
                        if (label is null) break;
                
                        label.text = $"{valueComponent.name} ({ObjectNames.NicifyVariableName(valueComponent.GetType().Name)}) ({index})";
                    }
                }
            });
        }

        private static VisualElement Build(
            SerializedObject serializedObject,
            string? title,
            IReadOnlyCollection<string>? propertiesExcluding)
        {
            var container = new AspidBox();

            if (!string.IsNullOrWhiteSpace(title))
                container.AddChild(new AspidLabel(title).SetMarginBottom(5));
            
            var targetType = serializedObject.targetObject.GetType();

            var router = new HeaderGroupRouter(container, targetType);
            var fieldMap = targetType.GetInstanceFieldMap();

            var enterChildren = true;
            var iterator = serializedObject.GetIterator();
            while (iterator.NextVisible(enterChildren))
            {
                enterChildren = false;
                BuildProperty(isGenerated: true);
            }

            enterChildren = true;
            iterator = serializedObject.GetIterator();
            while (iterator.NextVisible(enterChildren))
            {
                enterChildren = false;
                BuildProperty(isGenerated:false);
            }
            
            return container.SetDisplay(container.childCount > 0 ? DisplayStyle.Flex : DisplayStyle.None);
            
            void BuildProperty(bool isGenerated)
            {
                if (propertiesExcluding?.Contains(iterator.name) ?? false) return;
                
                fieldMap.TryGetValue(iterator.name, out var fieldInfo);
                if (fieldInfo?.IsDefined(typeof(GeneratedCodeAttribute)) != isGenerated) return;
                
                var field = GetPropertyField(fieldInfo, iterator.Copy());
                router.Add(field, fieldInfo);
            }
        }
        
        private static VisualElement GetPropertyField(FieldInfo? fieldInfo, SerializedProperty propertyCopy)
        {
            var fieldType = fieldInfo?.FieldType;

            if (IsMonoBinderType(fieldType))
            {
                var binderId = fieldInfo is not null
                    ? BinderIdUtility.FromFieldName(fieldInfo.Name)
                    : string.Empty;

                var assemblyQualifiedName = GetAssemblyQualifiedName(fieldInfo);
                return new MonoBinderPropertyField(propertyCopy, binderId, assemblyQualifiedName);
            }

            return new AspidPropertyField(propertyCopy);

            bool IsMonoBinderType(Type? type)
            {
                if (type is null) return false;
                if (type.IsArray) return IsMonoBinderType(type.GetElementType());
                return typeof(MonoBinder).IsAssignableFrom(type);
            }
        }

        private static string? GetAssemblyQualifiedName(FieldInfo? field)
        {
            if (field is null) return null;

            var requireBinder = field.GetCustomAttributes<RequireBinderAttribute>()
                .SelectMany(a => a.AssemblyQualifiedNames ?? Array.Empty<string>())
                .FirstOrDefault(name => !string.IsNullOrWhiteSpace(name));

            if (!string.IsNullOrWhiteSpace(requireBinder))
                return requireBinder;

            var fieldType = field.FieldType;
            var elementType = fieldType.IsArray ? fieldType.GetElementType() : fieldType;
            return elementType?.AssemblyQualifiedName;
        }
    }
}
