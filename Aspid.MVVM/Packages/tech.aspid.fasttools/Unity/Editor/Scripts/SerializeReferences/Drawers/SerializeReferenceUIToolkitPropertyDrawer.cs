using System;
using UnityEditor;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors
{
    internal static class SerializeReferenceUIToolkitPropertyDrawer
    {
        public static VisualElement Draw(string label, SerializedProperty property, params Type[] baseTypes)
            => Draw(label, property, baseTypes, out _);

        // The out overload hands the created field to callers that keep updating its base types after
        // creation (live member-referenced constraints — see TypeSelectorPropertyDrawer).
        internal static VisualElement Draw(string label, SerializedProperty property, Type[] baseTypes, out SerializeReferenceField field)
        {
            label = string.IsNullOrWhiteSpace(label) ? null : label;
            field = new SerializeReferenceField(label, property, baseTypes);
            return field;
        }
    }
}
