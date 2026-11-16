#nullable enable
using System.Linq;
using UnityEditor;
using UnityEngine;
using Aspid.FastTools.Editors;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Serialized binder View reference and its previous reference and label.
    /// </summary>
    public class MonoBinderViewProperty
    {
        /// <summary>
        /// Gets the serialized View reference.
        /// </summary>
        public SerializedProperty ValueProperty { get; private set; }
        
        /// <summary>
        /// Gets the serialized previous View record.
        /// </summary>
        public SerializedProperty PreviousProperty { get; private set; }
        
        /// <summary>
        /// Gets the View reference within the previous record.
        /// </summary>
        public SerializedProperty PreviousValueProperty { get; private set; }
        
        /// <summary>
        /// Gets the View label within the previous record.
        /// </summary>
        public SerializedProperty PreviousNameProperty { get; private set; }

        /// <summary>
        /// Gets or sets the View, also replacing the recorded reference and label.
        /// </summary>
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

        /// <summary>
        /// Gets the last recorded View; <see langword="null"/> when absent or destroyed.
        /// </summary>
        public IView? PreviousValue
        {
            get => PreviousValueProperty.objectReferenceValue as IView;
            private set => PreviousValueProperty.SetObjectReferenceAndApply(value as Component);
        }

        /// <summary>
        /// Gets the recorded View label, retained after destruction.
        /// </summary>
        public string PreviousName
        {
            get => PreviousNameProperty.stringValue;
            private set => PreviousNameProperty.SetStringAndApply(value);
        }
        
        /// <summary>
        /// Creates a wrapper for a binder View and its previous assignment.
        /// </summary>
        /// <param name="serializedObject">The serialized binder.</param>
        public MonoBinderViewProperty(SerializedObject serializedObject)
        {
            ValueProperty = serializedObject.FindProperty("__view");
            
            PreviousProperty = serializedObject.FindProperty("__previousView");
            PreviousNameProperty = PreviousProperty.FindPropertyRelative("_name");
            PreviousValueProperty = PreviousProperty.FindPropertyRelative("_view");
        }

        /// <summary>
        /// Clears the View unless it is on the binder GameObject or an ancestor.
        /// </summary>
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