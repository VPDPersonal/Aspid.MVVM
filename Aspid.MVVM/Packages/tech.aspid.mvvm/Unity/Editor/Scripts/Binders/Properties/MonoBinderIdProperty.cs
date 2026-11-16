#nullable enable
using UnityEditor;
using UnityEngine;
using System.Collections;
using Aspid.FastTools.Editors;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Serialized binder identifier and its previous value.
    /// </summary>
    public class MonoBinderIdProperty
    {
        /// <summary>
        /// Gets the serialized identifier.
        /// </summary>
        public SerializedProperty ValueProperty { get; }

        /// <summary>
        /// Gets the serialized previous identifier record.
        /// </summary>
        public SerializedProperty PreviousProperty { get; }

        /// <summary>
        /// Gets the identifier property within the previous record.
        /// </summary>
        public SerializedProperty PreviousValueProperty { get; }

        /// <summary>
        /// Gets or sets the identifier, also replacing the recorded previous value.
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
        /// Gets the last recorded identifier.
        /// </summary>
        public string PreviousValue
        {
            get => PreviousValueProperty.stringValue;
            private set => PreviousValueProperty.SetStringAndApply(value);
        }

        /// <summary>
        /// Creates a wrapper for a binder identifier and its previous value.
        /// </summary>
        /// <param name="serializedObject">The serialized binder.</param>
        public MonoBinderIdProperty(SerializedObject serializedObject)
        {
            ValueProperty = serializedObject.FindProperty("__id");
            PreviousProperty = serializedObject.FindProperty("__previousId");
            PreviousValueProperty = PreviousProperty.FindPropertyRelative("_id");
        }

        /// <summary>
        /// Clears an identifier whose View field no longer holds this binder.
        /// </summary>
        /// <remarks>
        /// Records the identifier before validation so a cleared assignment can still be displayed.
        /// </remarks>
        /// <param name="validViewProperty">The validated View reference.</param>
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