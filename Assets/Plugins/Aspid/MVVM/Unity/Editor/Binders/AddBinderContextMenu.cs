using System;
using UnityEditor;
using UnityEngine;
using System.Reflection;
using UnityEditor.Callbacks;
using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace Aspid.MVVM.Unity
{
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
            var types = FindTypesWithTargetPropertyTypeAttribute(target, property);

            foreach (var type in types)
            {
                menu.AddItem(new GUIContent($"{AddBinderText}/{type.Name}"), false, () =>
                {
                    if (target is Component component)
                        component.gameObject.AddComponent(type);
                }); 
            }
        }

        private static IReadOnlyList<Type> FindTypesWithTargetPropertyTypeAttribute(Object target, SerializedProperty property)
        {
            var propertyName = property.name;
            var targetType = target.GetType();
            var propertyType = property.GetPropertyType();
        
            var result = new List<Type>();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (var assembly in assemblies)
            {
                foreach (var type in assembly.GetTypes())
                {
                    var attributes = type.GetCustomAttributes<AddPropertyContextMenu>(true);

                    foreach (var attribute in attributes)
                    {
                        var serializePropertyName = attribute.SerializePropertyName;
                        
                        if (!string.IsNullOrWhiteSpace(serializePropertyName))
                        {
                            if (serializePropertyName == propertyName && attribute.Type.IsAssignableFrom(targetType))
                                result.Add(type);
                        }
                        else  if (attribute.Type.IsAssignableFrom(propertyType))
                        {
                            result.Add(type);
                        }
                    }
                }
            }

            return result;
        }
    }
}