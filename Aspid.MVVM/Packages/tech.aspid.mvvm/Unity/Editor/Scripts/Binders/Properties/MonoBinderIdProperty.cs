#nullable enable
using UnityEditor;
using UnityEngine;
using System.Collections;
using Aspid.FastTools.Editors;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// The binder's <c>Id</c> field as the Inspector works with it: the serialized value, and the copy kept so
    /// the previous id is still known after the current one is cleared.
    /// </summary>
    public class MonoBinderIdProperty
    {
        /// <summary>
        /// The serialized id itself.
        /// </summary>
        public SerializedProperty ValueProperty { get; }

        /// <summary>
        /// The serialized record of the last id that was set.
        /// </summary>
        public SerializedProperty PreviousProperty { get; }

        /// <summary>
        /// The id inside that record.
        /// </summary>
        public SerializedProperty PreviousValueProperty { get; }

        /// <summary>
        /// Gets or sets the id. Setting it also records the value as the previous one, so clearing the id later
        /// still leaves the Inspector able to say what it used to be.
        /// </summary>
        public string Value
        {
            get => ValueProperty.stringValue;
            set
            {
                PreviousValue = string.IsNullOrWhiteSpace(value) ? string.Empty : value;
                ValueProperty.SetStringAndApply(value);
            }
        }

        /// <summary>
        /// Gets the last id that was set.
        /// </summary>
        public string PreviousValue
        {
            get => PreviousValueProperty.stringValue;
            private set => PreviousValueProperty.SetStringAndApply(value);
        }

        /// <summary>
        /// Finds the id properties on the given binder.
        /// </summary>
        /// <param name="serializedObject">The serialized binder whose id is edited.</param>
        public MonoBinderIdProperty(SerializedObject serializedObject)
        {
            ValueProperty = serializedObject.FindProperty("__id");
            PreviousProperty = serializedObject.FindProperty("__previousId");
            PreviousValueProperty = PreviousProperty.FindPropertyRelative("_id");
        }

        /// <summary>
        /// Clears the id if the view no longer has a field by that id holding this binder.
        /// </summary>
        /// <param name="validViewProperty">The already-validated view the id is resolved against.</param>
        /// <remarks>
        /// An id survives the view being changed or the field being renamed, and would then point at nothing.
        /// The previous value is recorded before the check, so a cleared id can still be shown to the user.
        /// </remarks>
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