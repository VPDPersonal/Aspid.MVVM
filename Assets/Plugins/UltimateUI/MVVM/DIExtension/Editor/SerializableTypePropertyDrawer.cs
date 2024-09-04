using System;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace UltimateUI.MVVM.DIExtension
{
    [CustomPropertyDrawer(typeof(SerializableMonoScript),true)]
    public class SerializableTypePropertyDrawer : PropertyDrawer
    {
        private Type _filterType;
        private static readonly Dictionary<Type, MonoScript> _monoScriptCache = new();
            
        private static MonoScript GetMonoScript(Type aType)
        {
            if (aType == null) return null;
                
            if (_monoScriptCache.TryGetValue(aType, out var script) && script != null)
                return script;
                
            var scripts = Resources.FindObjectsOfTypeAll<MonoScript>();
                
            foreach(var s in scripts)
            {
                var type = s.GetClass();
                if (type != null) _monoScriptCache[type] = s;
                if (type == aType) script = s;
            }
                
            return script;
        }
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            InitializeFilterType();
            var typeName = property.FindPropertyRelative("_typeName");
            
            var type = Type.GetType(typeName.stringValue);
            var monoScript = GetMonoScript(type);
            EditorGUI.BeginChangeCheck();
            monoScript = (MonoScript)EditorGUI.ObjectField(position, label, monoScript, typeof(MonoScript), true);
            if (!EditorGUI.EndChangeCheck()) return;
            
            if (!monoScript) typeName.stringValue = "";
            else
            {
                var newType = monoScript.GetClass();
                if (newType != null && _filterType.IsAssignableFrom(newType)) typeName.stringValue = newType.AssemblyQualifiedName;
                else Debug.LogWarning("Dropped type does not derive or implement " + _filterType.Name);
            }
        }

        private void InitializeFilterType()
        {
            if (_filterType != null) return;
                
            var fieldType = fieldInfo.FieldType;
            if (fieldType.IsGenericType && fieldType.GetGenericTypeDefinition() == typeof(List<>))
            {
                // when used in a List<>, grab the actual type from the generic argument of the List
                fieldType = fieldInfo.FieldType.GetGenericArguments()[0];
            }
            else if (fieldType.IsArray)
            {
                // when used in an array, grab the actual type from the element type.
                fieldType = fieldType.GetElementType();
            }
            if (fieldType is { IsGenericType: true })
            {
                var types = fieldType.GetGenericArguments();
                if (types is { Length: 1 }) _filterType = types[0];
            }
            else _filterType = typeof(UnityEngine.Object);
        }
    }
}