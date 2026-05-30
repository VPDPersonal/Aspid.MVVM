using System;
using UnityEditor;
using UnityEngine;
using System.Linq;
using System.Reflection;
using UnityEditor.Callbacks;
using Aspid.FastTools.Editors;
using System.Collections.Generic;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Registers a Unity Editor context menu entry that allows adding compatible <see cref="MonoBinder"/> components
    /// directly from a serialized properties right-click menu.
    /// </summary>
    internal static class AddBinderContextMenu
    {
        private const string AddBinderText = "Add Binder";
        
        private static Context[] _contexts;
    
        [DidReloadScripts]
        [InitializeOnLoadMethod]
        [InitializeOnEnterPlayMode]
        private static void Initialize()
        {
            if (_contexts is not null) return;
        
            _contexts = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(SafeGetTypes)
                .Where(Context.IsValid)
                .Select(type => new Context(type))
                .ToArray();
            
            EditorApplication.contextualPropertyMenu -= OnContextualPropertyMenu;
            EditorApplication.contextualPropertyMenu += OnContextualPropertyMenu;
        }

        private static IEnumerable<Type> SafeGetTypes(Assembly assembly)
        {
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {
                return ex.Types.Where(type => type != null);
            }
        }

        private static void OnContextualPropertyMenu(GenericMenu menu, SerializedProperty property)
        {
            menu.AddSeparator(path: "/");
            var target = property.serializedObject.targetObject;
            var results = FindTypesWithTargetPropertyTypeAttribute(target, property);

            foreach (var item in results)
            {
                foreach (var type in item.Types)
                {
                    menu.AddItem(new GUIContent(text: $"{AddBinderText}/{item.Name}"), false, () =>
                    {
                        if (target is Component component)
                            component.gameObject.AddComponent(type);
                    }); 
                }
            }
        }

        private static IReadOnlyList<Result> FindTypesWithTargetPropertyTypeAttribute(Object target, SerializedProperty property)
        {
            var propertyName = property.name;
            var targetType = target.GetType();
            var propertyType = property.GetPropertyType();
            
            if (propertyType is null) 
                return Array.Empty<Result>();
        
            var results = new List<Result>();

            foreach (var context in _contexts)
            {
                var name = context.Name;
                var types = new List<Type>();
                
                foreach (var contextMenu in context.ContextMenus)
                {
                    foreach (var serializePropertyName in contextMenu.SerializePropertyNames)
                    {
                        if (string.IsNullOrWhiteSpace(serializePropertyName)) continue;
                        if (serializePropertyName != propertyName) continue;
                        if (!contextMenu.Type.IsAssignableFrom(targetType)) continue;
                            
                        types.Add(contextMenu.Type);
                    }
                }

                types.AddRange(collection: context.ContextMenuByTypes
                    .Select(c => c.Type)
                    .Where(type => type.IsAssignableFrom(propertyType)));

                results.Add(new Result(name, types));
            }

            return results;
        }
        
        private readonly struct Result
        {
            public readonly string Name;
            public readonly IReadOnlyList<Type> Types;
            
            public Result(string name, IReadOnlyList<Type> types)
            {
                Name = name;
                Types = types;
            }
        }
        
        private readonly struct Context
        {
            private const bool Inherit = true;
            
            public readonly string Name;
            public readonly IReadOnlyList<ContextMenu> ContextMenus;
            public readonly IReadOnlyList<ContextMenuByType> ContextMenuByTypes;
            
            public Context(Type type)
            {
                Name = GetName(type);
                
                ContextMenus = type
                    .GetCustomAttributes<AddBinderContextMenuAttribute>(Inherit)
                    .Select(attribute => new ContextMenu(attribute))
                    .ToArray();

                ContextMenuByTypes = type
                    .GetCustomAttributes<AddBinderContextMenuByTypeAttribute>(Inherit)
                    .Select(attribute => new ContextMenuByType(attribute))
                    .ToArray();
            }

            private static string GetName(Type type)
            {
                var addComponentMenu = type.GetCustomAttribute<AddComponentMenu>();
                var name = addComponentMenu?.componentMenu;
                    
                if (!string.IsNullOrWhiteSpace(name))
                    name = name[(name.LastIndexOf('/') + 1)..];
                    
                if (string.IsNullOrWhiteSpace(name))
                    name = type.Name;

                return name;
            }

            public static bool IsValid(Type type)
            {
                return type.IsDefined(typeof(AddBinderContextMenuAttribute), Inherit) 
                    || type.IsDefined(typeof(AddBinderContextMenuByTypeAttribute), Inherit);
            }
        }

        private readonly struct ContextMenu
        {
            public readonly Type Type;
            public readonly IReadOnlyList<string> SerializePropertyNames;
            
            public ContextMenu(AddBinderContextMenuAttribute attribute)
            {
                Type = attribute.Type;
                SerializePropertyNames = attribute.SerializePropertyNames;;
            }
        }

        private readonly struct ContextMenuByType
        {
            public readonly Type Type;

            public ContextMenuByType(AddBinderContextMenuByTypeAttribute attribute) =>
                Type = attribute.Type;
        }
    }
}