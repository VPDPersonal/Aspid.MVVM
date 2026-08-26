using UnityEditor;
using UnityEngine;
using System.Linq;
using System.Reflection;
using System.Collections;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// <see cref="PropertyDrawer"/> for <see cref="UsedInModesAttribute"/>: draws the field as
    /// usual, disabled while the hosting binder is bound in a mode the field is not used under.
    /// </summary>
    /// <remarks>
    /// The field keeps its own drawer — this one only wraps it in a disabled scope, so it composes
    /// with types that draw themselves.
    /// </remarks>
    [CustomPropertyDrawer(typeof(UsedInModesAttribute))]
    internal sealed class UsedInModesDrawer : PropertyDrawer
    {
        private const string DisabledNote = "Not used in the current Mode.";

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var disabled = IsDisabled(property);
            
            if (disabled)
            {
                var tooltip = string.IsNullOrEmpty(label.tooltip)
                    ? DisabledNote 
                    : $"{label.tooltip}\n{DisabledNote}";
                
                label = new GUIContent(label.text, label.image, tooltip);
            }

            using (new EditorGUI.DisabledScope(disabled))
                EditorGUI.PropertyField(position, property, label, includeChildren: true);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) =>
            EditorGUI.GetPropertyHeight(property, label, includeChildren: true);

        private bool IsDisabled(SerializedProperty property)
        {
            if (attribute is not UsedInModesAttribute { Modes: { Count: > 0 } modes }) return false;
            if (FindHostBinder(property) is not { } binder) return false;

            return modes.All(mode => mode != binder.Mode);
        }

        /// <summary>
        /// Finds the nearest <see cref="IBinder"/> above the drawn property, or
        /// <see langword="null"/> when the property is hosted outside a binder.
        /// </summary>
        /// <remarks>
        /// Owners are walked from the serialized object inward, so a binder nested in another
        /// binder wins over the one holding it.
        /// </remarks>
        private static IBinder FindHostBinder(SerializedProperty property)
        {
            object current = property.serializedObject.targetObject;
            var binder = current as IBinder;

            var path = property.propertyPath.Replace(".Array.data[", "[");
            var segments = path.Split('.');

            // The last segment is the marked field itself — its owners are what matters.
            for (var i = 0; i < segments.Length - 1 && current is not null; i++)
            {
                current = GetSegmentValue(current, segments[i]);
                if (current is IBinder host) binder = host;
            }

            return binder;
        }

        private static object GetSegmentValue(object target, string segment)
        {
            var bracket = segment.IndexOf('[');
            var name = bracket < 0 ? segment : segment[..bracket];

            var value = GetFieldValue(target, name);
            if (bracket < 0) return value;

            var index = int.Parse(segment[(bracket + 1)..^1]);
            return value is IList list && index < list.Count ? list[index] : null;
        }

        private static object GetFieldValue(object target, string name)
        {
            const BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly;

            for (var type = target.GetType(); type is not null; type = type.BaseType)
            {
                if (type.GetField(name, flags) is { } field)
                    return field.GetValue(target);
            }

            return null;
        }
    }
}
