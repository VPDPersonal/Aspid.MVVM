#nullable enable
using System.Linq;
using UnityEditor;
using UnityEngine;
using Aspid.FastTools.Editors;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public class MonoBinderViewProperty
    {
        public SerializedProperty ValueProperty { get; private set; }
        
        public SerializedProperty PreviousProperty { get; private set; }
        
        public SerializedProperty PreviousValueProperty { get; private set; }
        
        public SerializedProperty PreviousNameProperty { get; private set; }

        public IView? Value
        {
            get => ValueProperty.objectReferenceValue as IView;
            set
            {
                if (value is null)
                {
                    PreviousValue = null;
                    PreviousName = string.Empty;
                }
                else
                {
                    PreviousValue = value;
                    PreviousName = BinderViewData.GetViewName(value as Component);
                }
                
                ValueProperty.SetObjectReferenceAndApply(value as Component);
            }
        }

        public IView? PreviousValue
        {
            get => PreviousValueProperty.objectReferenceValue as IView;
            private set => PreviousValueProperty.SetObjectReferenceAndApply(value as Component);
        }

        public string PreviousName
        {
            get => PreviousNameProperty.stringValue;
            private set => PreviousNameProperty.SetStringAndApply(value);
        }
        
        public MonoBinderViewProperty(SerializedObject serializedObject)
        {
            ValueProperty = serializedObject.FindProperty("__view");
            
            PreviousProperty = serializedObject.FindProperty("__previousView");
            PreviousNameProperty = PreviousProperty.FindPropertyRelative("_name");
            PreviousValueProperty = PreviousProperty.FindPropertyRelative("_view");
        }

        public void Validate()
        {
            if (Value is null) return;

            PreviousValue = Value;
            var target = (Component)ValueProperty.serializedObject.targetObject;

            for (var parent = target.transform; parent is not null; parent = parent.parent)
            {
                if (parent.GetComponents<IView>().Any(view => Value == view))
                {
                    return;
                }
            }

            Value = null;
        }
    }
}