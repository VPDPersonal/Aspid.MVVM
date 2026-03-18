#nullable enable
using UnityEditor;
using UnityEngine;
using System.Collections;
using Aspid.FastTools.Editors;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public class MonoBinderIdProperty
    {
        public SerializedProperty ValueProperty { get; private set; }
        
        public SerializedProperty PreviousProperty { get; private set; }
        
        public SerializedProperty PreviousValueProperty { get; private set; }

        public string Value
        {
            get => ValueProperty.stringValue;
            set
            {
                PreviousValue = string.IsNullOrWhiteSpace(value) ? string.Empty : value;
                ValueProperty.SetStringAndApply(value);
            }
        }

        public string PreviousValue
        {
            get => PreviousValueProperty.stringValue;
            private set => PreviousValueProperty.SetStringAndApply(value);
        }
        
        public MonoBinderIdProperty(SerializedObject serializedObject)
        {
            ValueProperty = serializedObject.FindProperty("__id");
            PreviousProperty = serializedObject.FindProperty("__previousId");
            PreviousValueProperty = PreviousProperty.FindPropertyRelative("_id");
        }
        
        public void Validate(MonoBinderViewProperty validViewProperty)
        {
            if (string.IsNullOrWhiteSpace(Value)) return;
            
            PreviousValue = Value;
            var view = validViewProperty.Value;
            var target = (Component)ValueProperty.serializedObject.targetObject;
                
            if (view is not null && view.TryGetRequireBinderFieldsById(Value, out var field))
            {
                if (field!.FieldType.IsArray)
                {
                    foreach (var item in (IEnumerable)field.GetValue(field.FieldContainerObj))
                    {
                        if (item as Component == target)
                        {
                            return;
                        }
                    }
                }
                else
                {
                    if ((MonoBinder)field.GetValue(field.FieldContainerObj) == target)
                    {
                        return;
                    }
                }
            }
            
            ValueProperty.stringValue = string.Empty;
        }
    }
}