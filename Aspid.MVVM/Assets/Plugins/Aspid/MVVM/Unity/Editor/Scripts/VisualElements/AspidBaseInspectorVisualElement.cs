#nullable enable
using System;
using UnityEditor;
using System.Linq;
using UnityEngine;
using System.Reflection;
using Aspid.UnityFastTools;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public sealed class AspidBaseInspectorVisualElement : VisualElement
    {
        public AspidBaseInspectorVisualElement(SerializedObject serializedObject, string? title, IReadOnlyCollection<string>? propertiesExcluding = null)
        {
            var container = Build(serializedObject, title, propertiesExcluding);
            style.display = container.style.display;
            Add(container);
        }

        private static VisualElement Build(SerializedObject serializedObject, string? title, IReadOnlyCollection<string>? propertiesExcluding)
        {
            var container = new AspidContainer();

            if (!string.IsNullOrWhiteSpace(title))
                container.AddChild(new AspidTitle(title));

            var count = 0;
            var enterChildren = true;
            var targetType = serializedObject.targetObject.GetType();
            var iterator = serializedObject.GetIterator();

            while (iterator.NextVisible(enterChildren))
            {
                enterChildren = false;
                if (propertiesExcluding?.Contains(iterator.name) ?? false) continue;

                var marginTop = count++ > 0 ? 4 : 0;

                VisualElement field;
                var fieldType = targetType.GetField(iterator.name, bindingAttr: BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)?.FieldType;

                if (IsMonoBinderType(fieldType))
                {
                    var elementType = fieldType.IsArray ? fieldType.GetElementType() : fieldType;
                    field = new MonoBinderPropertyField(iterator.Copy(), elementType?.AssemblyQualifiedName ?? string.Empty).SetMargin(top: marginTop);
                }
                else
                {
                    var propertyField = new AspidPropertyField(iterator).SetMargin(top: marginTop);
                    field = propertyField;
                }
                
                field.RegisterCallback<GeometryChangedEvent>(_ =>
                {
                    var objectField = field.Q<ObjectField>();
                    if (objectField?.value is not Component valueComponent) return;
                    if (objectField.childCount < 2) return;

                    var components = valueComponent.GetComponents(objectField.objectType);
                    if (components.Length < 2) return;

                    var index = 0;

                    foreach (var component in components)
                    {
                        index++;
                        if (valueComponent != component) continue;

                        var label = objectField[1].Q<Label>();
                        if (label is null) break;

                        label.text = $"{valueComponent.name} ({ObjectNames.NicifyVariableName(valueComponent.GetType().Name)}) ({index})";
                    }
                });

                container.AddChild(field);
            }

            container.style.display = count > 0 ? DisplayStyle.Flex : DisplayStyle.None;
            return container;
        }

        private static bool IsMonoBinderType(Type? type)
        {
            if (type is null) return false;
            if (type.IsArray) return IsMonoBinderType(type.GetElementType());
            return typeof(MonoBinder).IsAssignableFrom(type);
        }
    }
}