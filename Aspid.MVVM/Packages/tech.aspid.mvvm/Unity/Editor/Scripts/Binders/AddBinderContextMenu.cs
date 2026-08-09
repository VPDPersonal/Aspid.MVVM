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
            var target = property.serializedObject.targetObject;

            // Binders are added via AddComponent, which requires a GameObject; skip non-Component
            // targets (e.g. ScriptableObject assets) so the menu never shows a dead entry.
            if (target is not Component component)
                return;

            var results = FindTypesWithTargetPropertyTypeAttribute(target, property);
            if (results.Count == 0)
                return;

            menu.AddSeparator(path: "/");

            foreach (var item in results)
            {
                var binderType = item.Type;

                menu.AddItem(new GUIContent(text: $"{AddBinderText}/{item.Name}"), false, () =>
                    component.gameObject.AddComponent(binderType));
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
                // Match against the attribute's target/value type, but never add that type:
                // the menu must add the binder itself (context.Type). The property-type scan is
                // only reached when the target-property match fails, thanks to || short-circuiting.
                var matches = context.ContextMenus.Any(contextMenu =>
                        contextMenu.Type.IsAssignableFrom(targetType) &&
                        contextMenu.SerializePropertyNames.Any(serializePropertyName =>
                            !string.IsNullOrWhiteSpace(serializePropertyName) &&
                            serializePropertyName == propertyName))
                    || context.ContextMenuByTypes.Any(contextMenu =>
                        contextMenu.Type.IsAssignableFrom(propertyType));

                if (matches)
                    results.Add(new Result(context.Name, context.Type));
            }

            return results;
        }
        
        private readonly struct Result
        {
            public readonly string Name;
            public readonly Type Type;

            public Result(string name, Type type)
            {
                Name = name;
                Type = type;
            }
        }
        
        private readonly struct Context
        {
            private const bool Inherit = true;
            
            public readonly Type Type;
            public readonly string Name;
            public readonly IReadOnlyList<ContextMenu> ContextMenus;
            public readonly IReadOnlyList<ContextMenuByType> ContextMenuByTypes;

            public Context(Type type)
            {
                Type = type;
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
                // The menu adds the type via AddComponent, which only succeeds for a concrete,
                // non-generic MonoBehaviour. Skip anything else so a click can never fail at runtime.
                if (type.IsAbstract || type.IsGenericTypeDefinition || !typeof(MonoBehaviour).IsAssignableFrom(type))
                    return false;

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