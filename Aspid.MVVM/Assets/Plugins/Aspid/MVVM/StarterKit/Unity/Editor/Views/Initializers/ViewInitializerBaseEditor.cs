using System;
using UnityEditor;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    // TODO Aspid.MVVM – Refactor
    public abstract class ViewInitializerBaseEditor : Editor
    {
        protected static bool DrawInitializeComponent<TInterface>(SerializedProperty property, string label)
            where TInterface : class
        {
            EditorGUILayout.PropertyField(property, new GUIContent(label));
            
            if (property.isArray)
            {
                var result = property.arraySize is not 0;
                for (var i = 0; i < property.arraySize; i++)
                {
                    var elementProperty = property.GetArrayElementAtIndex(i);
                    if (!ValidateInitializeComponent<TInterface>(elementProperty))
                        result = false;
                }

                return result;
            }
            
            return ValidateInitializeComponent<TInterface>(property);
        }

        protected static string GetInitializeComponentName(SerializedProperty property)
        {
            var resolve = GetResolve(property);
            
            switch (resolve)
            {
                case ResolveType.References:
                    {
                        var referencesProperty = GetReferencesProperty(property);
                        var typeName = referencesProperty.managedReferenceValue?.GetType().Name;
                        
                        return !string.IsNullOrWhiteSpace(typeName) 
                            ? $" ({typeName})" 
                            : string.Empty;
                    }
                case ResolveType.ScriptableObject:
                    {
                        var scriptableProperty = GetScriptableProperty(property);
                        
                        return scriptableProperty.objectReferenceValue 
                            ? $" ({scriptableProperty.objectReferenceValue.GetType().Name})" 
                            : string.Empty;
                    }
#if ASPID_MVVM_ZENJECT_INTEGRATION || ASPID_MVVM_VCONTAINER_INTEGRATION
                case ResolveType.Di:
                    {
                        var typeNameProperty = GetTypeNameProperty(property);
                        var typeName = Type.GetType(typeNameProperty.stringValue)?.Name;
                        
                        return !string.IsNullOrWhiteSpace(typeName) 
                            ? $" ({typeName})" 
                            : string.Empty;
                    }
#endif
                case ResolveType.Mono:
                default:
                    {
                        var monoProperty = GetMonoProperty(property);
                        
                        return monoProperty.objectReferenceValue
                            ? $" ({monoProperty.objectReferenceValue.GetType().Name})"
                            : string.Empty;
                    }
            }
        }

        private static bool ValidateInitializeComponent<TInterface>(SerializedProperty property)
            where TInterface : class
        {
            var resolve = GetResolve(property);

            switch (resolve)
            {
                case ResolveType.References:
                    {
                        var referencesProperty = GetReferencesProperty(property);
                        return referencesProperty.managedReferenceValue is not null;
                    }
                case ResolveType.ScriptableObject:
                    {
                        var scriptableProperty = GetScriptableProperty(property);
                        if (scriptableProperty.objectReferenceValue is not TInterface)
                            scriptableProperty.objectReferenceValue = null;

                        return scriptableProperty.objectReferenceValue;
                    }
#if ASPID_MVVM_ZENJECT_INTEGRATION || ASPID_MVVM_VCONTAINER_INTEGRATION
                case ResolveType.Di:
                    {
                        var typeNameProperty = GetTypeNameProperty(property);
                        return !string.IsNullOrWhiteSpace(typeNameProperty.stringValue);
                    }
#endif
                case ResolveType.Mono:
                default:
                    {
                        var monoProperty = GetMonoProperty(property);
                        if (monoProperty.objectReferenceValue is not null and not TInterface)
                            monoProperty.objectReferenceValue = ((Component)monoProperty.objectReferenceValue).GetComponent<TInterface>() as UnityEngine.Object;

                        return monoProperty.objectReferenceValue;
                    }
            }
        }

        private static SerializedProperty GetMonoProperty(SerializedProperty property) =>
            property.FindPropertyRelative("_mono");
        
        private static SerializedProperty GetScriptableProperty(SerializedProperty property) =>
            property.FindPropertyRelative("_scriptableObject");
        
        private static SerializedProperty GetReferencesProperty(SerializedProperty property) =>
            property.FindPropertyRelative("_reference");
        
        private static SerializedProperty GetTypeNameProperty(SerializedProperty property) =>
            property.FindPropertyRelative("_typeName");
        
        private static ResolveType GetResolve(SerializedProperty property) =>
            (ResolveType)property.FindPropertyRelative("_resolve").enumValueIndex;
    }
}