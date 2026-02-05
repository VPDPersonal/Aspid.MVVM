using System;
using UnityEditor;
using UnityEngine;
using System.Reflection;
using UnityEditor.Callbacks;
using System.Collections.Generic;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    // TODO Aspid.MVVM Unity – Refactor
    // TODO Aspid.MVVM Unity – Write summary
    internal static class AddBinderContextMenu
    {
        private const string AddBinderText = "Add Binder";

        private static bool _initialized;
    
        [DidReloadScripts]
        [InitializeOnLoadMethod]
        [InitializeOnEnterPlayMode]
        public static void Initialize()
        {
            if (_initialized) return;
            _initialized = true;
        
            EditorApplication.contextualPropertyMenu -= OnContextualPropertyMenu;
            EditorApplication.contextualPropertyMenu += OnContextualPropertyMenu;
        }
    
        private static void OnContextualPropertyMenu(GenericMenu menu, SerializedProperty property)
        {
            menu.AddSeparator("/");
            var target = property.serializedObject.targetObject;
            var items = FindTypesWithTargetPropertyTypeAttribute(target, property);

            foreach (var item in items)
            {
                menu.AddItem(new GUIContent($"{AddBinderText}/{item.Item2}"), false, () =>
                {
                    if (target is Component component)
                        component.gameObject.AddComponent(item.Item1);
                }); 
            }
        }

        private static IReadOnlyList<(Type, string)> FindTypesWithTargetPropertyTypeAttribute(Object target, SerializedProperty property)
        {
            var propertyName = property.name;
            var targetType = target.GetType();
            var propertyType = property.GetPropertyType();
            
            if (propertyType is null) 
                return Array.Empty<(Type, string)>();
        
            var result = new List<(Type, string)>();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (var assembly in assemblies)
            {
                foreach (var type in assembly.GetTypes())
                {
                    var addComponentMenu = type.GetCustomAttribute<AddComponentMenu>();
                    var name = addComponentMenu?.componentMenu;
                    
                    if (!string.IsNullOrWhiteSpace(name))
                        name = name[(name.LastIndexOf('/') + 1)..];
                    
                    if (string.IsNullOrWhiteSpace(name))
                        name = type.Name;
                    
                    foreach (var attribute in type.GetCustomAttributes<AddBinderContextMenuAttribute>(true))
                    {
                        var serializePropertyNames = attribute.SerializePropertyNames;
                        
                        foreach (var serializePropertyName in serializePropertyNames)
                        {
                            if (string.IsNullOrWhiteSpace(serializePropertyName)) continue;
                            
                            if (serializePropertyName == propertyName && attribute.Type.IsAssignableFrom(targetType))
                                result.Add((type, name));
                        }
                    }

                    foreach (var attribute in type.GetCustomAttributes<AddBinderContextMenuByTypeAttribute>(true))
                    {
                        if (attribute.Type.IsAssignableFrom(propertyType))
                            result.Add((type, name));
                    }
                }
            }

            return result;
        }
    }
}