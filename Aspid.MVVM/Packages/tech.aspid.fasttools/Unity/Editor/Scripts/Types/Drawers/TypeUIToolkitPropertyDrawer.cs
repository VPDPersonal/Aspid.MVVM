using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Aspid.FastTools.Editors;
using Aspid.FastTools.UIElements;
using Aspid.FastTools.SerializeReferences.Editors;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Types.Editors
{
    internal static class TypeUIToolkitPropertyDrawer
    {
        internal static VisualElement Draw(
            string label,
            SerializedProperty property,
            TypeAllow allow = TypeAllow.All,
            params Type[] types)
            => Draw(label, property, allow, types, out _);

        // The out overload hands the created field to callers that keep updating its Types after
        // creation (live member-referenced constraints — see TypeSelectorPropertyDrawer).
        internal static VisualElement Draw(
            string label,
            SerializedProperty property,
            TypeAllow allow,
            Type[] types,
            out InspectorTypeField field)
        {
            label = string.IsNullOrWhiteSpace(label) ? null : label;

            field = new InspectorTypeField(label, property)
            {
                Allow = allow,
                Types = types,
            };

            if (!SerializeReferenceRequiredGate.TryGetRequired(property, out _))
                return field;

            var container = new VisualElement().AddChild(field);
            var notice = new SerializeReferenceNotice();

            container.TrackPropertyValue(property, Refresh);
            Refresh(property.Persistent());

            return container;

            void Refresh(SerializedProperty current)
            {
                if (!SerializeReferenceRequiredGate.IsViolation(current))
                {
                    notice.RemoveFromHierarchy();
                    return;
                }

                notice.Set(
                    message: "Required type is not set",
                    actionText: string.Empty,
                    detail: "This [TypeSelector] field is marked required but has no type. Pick a type from the dropdown.",
                    onAction: null);

                if (notice.parent is null) container.AddChild(notice);
            }
        }
    }
}
