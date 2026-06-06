using System;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [CustomPropertyDrawer(typeof(SerializableMonoScript),true)]
    public class SerializableTypePropertyDrawer : PropertyDrawer
    {
        private Type _filterType;
        private static readonly Dictionary<Type, MonoScript> _monoScriptCache = new();
            
        private static MonoScript GetMonoScript(Type monoScriptType)
        {
            if (monoScriptType == null) return null;
            if (_monoScriptCache.TryGetValue(monoScriptType, out var script) && script != null) return script;
                
            var scripts = Resources.FindObjectsOfTypeAll<MonoScript>();
                
            foreach(var monoScript in scripts)
            {
                var type = monoScript.GetClass();
                if (type != null) _monoScriptCache[type] = monoScript;
                if (type == monoScriptType) script = monoScript;
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
            var type = SerializableUtility.GetGenericArgumentFromFieldType(fieldInfo, out var isGeneric);
            
            if (isGeneric) _filterType = type ?? _filterType;
            else _filterType = typeof(UnityEngine.Object);
        }
    }
}