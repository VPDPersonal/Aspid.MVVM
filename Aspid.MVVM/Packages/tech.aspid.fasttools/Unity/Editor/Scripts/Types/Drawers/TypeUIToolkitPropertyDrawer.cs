using System;
using UnityEditor;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Types.Editors
{
    internal static class TypeUIToolkitPropertyDrawer
    {
        public static VisualElement Draw(
            string label,
            SerializedProperty property,
            TypeAllow allow = TypeAllow.All,
            params Type[] types)
        {
            label = string.IsNullOrWhiteSpace(label) ? null : label;

            return new InspectorTypeField(label, property)
            {
                Allow = allow,
                Types = types
            };
        }
    }
}
