using System;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace Aspid.UI.MVVM.StarterKit.Utilities
{
    [CustomPropertyDrawer(typeof(SerializableInterface<>), true)]
    public class SerializableInterfacePropertyDrawer : PropertyDrawer
    {
        private Type _genericType;
        private readonly List<Component> _list = new();
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (_genericType == null)
            {
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

                var types = fieldType?.GetGenericArguments();
                if (types is { Length: 1 }) _genericType = types[0];
            }
            
            var obj = property.FindPropertyRelative("_obj");
            EditorGUI.BeginChangeCheck();
            var newObj = EditorGUI.ObjectField(position, label, obj.objectReferenceValue, typeof(Object), true);
            if (!EditorGUI.EndChangeCheck()) return;
            
            if (newObj == null) obj.objectReferenceValue = null;
            else if (_genericType != null && _genericType.IsInstanceOfType(newObj)) obj.objectReferenceValue = newObj;
            else if (newObj is GameObject go)
            {
                _list.Clear();
                go.GetComponents(_genericType, _list);
                if (_list.Count == 1) obj.objectReferenceValue = _list[0];
                else
                {
                    var m = new GenericMenu();
                    var n = 1;
                    
                    foreach (var item in _list)
                    {
                        m.AddItem(new GUIContent(n++ + " " + item.GetType().Name), false, a =>
                        {
                            obj.objectReferenceValue = (Object)a;
                            obj.serializedObject.ApplyModifiedProperties();
                        }, item);
                    }
                    
                    m.ShowAsContext();
                }
            }
            else Debug.LogWarning("Dragged object is not compatible with " + _genericType.Name);
        }
    }
}
