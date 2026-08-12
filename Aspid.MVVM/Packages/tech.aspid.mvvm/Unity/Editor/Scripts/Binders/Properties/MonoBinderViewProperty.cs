#nullable enable
using System.Linq;
using UnityEditor;
using UnityEngine;
using Aspid.FastTools.Editors;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// The binder's <c>View</c> field as the Inspector works with it: the serialized reference, and the copy
    /// kept so a view that has been removed can still be named.
    /// </summary>
    public class MonoBinderViewProperty
    {
        /// <summary>
        /// The serialized view reference itself.
        /// </summary>
        public SerializedProperty ValueProperty { get; private set; }
        
        /// <summary>
        /// The serialized record of the last view that was set.
        /// </summary>
        public SerializedProperty PreviousProperty { get; private set; }
        
        /// <summary>
        /// The view reference inside that record.
        /// </summary>
        public SerializedProperty PreviousValueProperty { get; private set; }
        
        /// <summary>
        /// The label of that view, kept because the reference alone cannot be named once it is gone.
        /// </summary>
        public SerializedProperty PreviousNameProperty { get; private set; }

        /// <summary>
        /// Gets or sets the bound view. Setting it also records the view and its label as the previous ones.
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
        /// Gets the last view that was set, which may itself have been destroyed since.
        /// </summary>
        public IView? PreviousValue
        {
            get => PreviousValueProperty.objectReferenceValue as IView;
            private set => PreviousValueProperty.SetObjectReferenceAndApply(value as Component);
        }

        /// <summary>
        /// Gets the label of the last view that was set. Survives the view's destruction, which is the point.
        /// </summary>
        public string PreviousName
        {
            get => PreviousNameProperty.stringValue;
            private set => PreviousNameProperty.SetStringAndApply(value);
        }
        
        /// <summary>
        /// Finds the view properties on the given binder.
        /// </summary>
        /// <param name="serializedObject">The serialized binder whose view is edited.</param>
        public MonoBinderViewProperty(SerializedObject serializedObject)
        {
            ValueProperty = serializedObject.FindProperty("__view");
            
            PreviousProperty = serializedObject.FindProperty("__previousView");
            PreviousNameProperty = PreviousProperty.FindPropertyRelative("_name");
            PreviousValueProperty = PreviousProperty.FindPropertyRelative("_view");
        }

        /// <summary>
        /// Clears the view reference if it no longer points at a live view.
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