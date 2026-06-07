using System;
using UnityEditor;
using System.Reflection;
using Aspid.FastTools.Editors;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Ids.Editors
{
    internal sealed class IdStructDrawerContext
    {
        public readonly string Label;
        public readonly Type FieldType;
        public readonly Type DeclaringType;
        public readonly SerializedProperty Property;
        public readonly SerializedProperty IntProperty;
        public readonly SerializedProperty StringProperty;

        public SerializedObject SerializedObject => Property.serializedObject;

        public IdStructDrawerContext(
            string label,
            FieldInfo fieldInfo,
            SerializedProperty property)
        {
            Label = label;
            FieldType = fieldInfo.FieldType;
            DeclaringType = fieldInfo.DeclaringType;

            Property = property.Persistent();
            IntProperty = Property.FindPropertyRelative(Constants.IntIdFieldName);
            StringProperty = Property.FindPropertyRelative(Constants.StringIdFieldName);
        }

        public void OpenRegistryAsset()
        {
            var registry = FindRegistry();
            if (registry is null) return;
            
            EditorGUIUtility.PingObject(registry);
            Selection.activeObject = registry;
        }
        
        public IdRegistry GetOrCreate() =>
            IdRegistryResolver.GetOrCreate(FieldType);
        
        public IdRegistry FindRegistry() =>
            IdRegistryResolver.Find(FieldType);
        
        public IdRegistry Create() =>
            IdRegistryResolver.Create(FieldType);
        
        public string GetCurrentAssetGuid()
        {
            var path = AssetDatabase.GetAssetPath(SerializedObject.targetObject);
            return string.IsNullOrEmpty(path) ? string.Empty : AssetDatabase.AssetPathToGUID(path);
        }
    }
}
