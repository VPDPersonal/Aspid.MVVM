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

                menu.AddItem(new GUIContent(text: item.Name), false, () =>
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
                var matchingMenu = context.ContextMenus.FirstOrDefault(contextMenu =>
                    contextMenu.Type.IsAssignableFrom(targetType)
                    && contextMenu.SerializePropertyNames.Any(name =>
                        !string.IsNullOrWhiteSpace(name) && name == propertyName));

                if (matchingMenu.Type is not null)
                {
                    var path = !string.IsNullOrWhiteSpace(matchingMenu.Path)
                        ? matchingMenu.Path
                        : string.IsNullOrWhiteSpace(matchingMenu.SubPath)
                            ? $"{AddBinderText}/{context.Name}"
                            : $"{AddBinderText}/{matchingMenu.SubPath}/{context.Name}";

                    results.Add(new Result(path, context.Type));
                }
                else if (context.ContextMenuByTypes.Any(menu => menu.Type.IsAssignableFrom(propertyType)))
                {
                    results.Add(new Result($"{AddBinderText}/{context.Name}", context.Type));
                }
            }

            return results;
        }
        
        private readonly struct Result
        {
            public readonly Type Type;
            public readonly string Name;

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
            public readonly string Path;
            public readonly string SubPath;
            public readonly IReadOnlyList<string> SerializePropertyNames;
            
            public ContextMenu(AddBinderContextMenuAttribute attribute)
            {
                Type = attribute.Type;
                Path = attribute.Path;
                SubPath = attribute.SubPath;
                SerializePropertyNames = attribute.SerializePropertyNames;
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