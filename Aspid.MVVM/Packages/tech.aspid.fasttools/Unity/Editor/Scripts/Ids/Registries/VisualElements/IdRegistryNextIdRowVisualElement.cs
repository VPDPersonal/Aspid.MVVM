using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Aspid.FastTools.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Ids.Editors
{
    /// <summary>
    /// Renders the manual Next-Id field (a <see cref="PropertyField"/>) plus its reuse-warning icon.
    /// The bound <see cref="SerializedProperty"/> must outlive this element.
    /// </summary>
    internal sealed class IdRegistryNextIdRowVisualElement : VisualElement
    {
        public event Action<int> ValueChanged;

        public IdRegistryNextIdRowVisualElement(SerializedProperty nextIdProp, Func<int, NextIdWarning> resolveWarning)
        {
            var warningImage = new Image();
            var field = new PropertyField(nextIdProp)
                .SetTooltip("Id that will be assigned to the next Add operation. Manual override is allowed.");

            // PropertyField writes via SerializedObject — Unity already records the Undo step.
            // We just refresh the warning icon and fan out ValueChanged for cache invalidation.
            field.RegisterValueChangeCallback(e =>
            {
                var value = e.changedProperty.intValue;
                ApplyWarning(value);
                ValueChanged?.Invoke(value);
            });

            this.AddChild(new VisualElement()
                .AddChild(field)
                .AddChild(warningImage));

            ApplyWarning(nextIdProp.intValue);
            return;

            void ApplyWarning(int value)
            {
                var warning = resolveWarning(value);
                warningImage.SetEnabled(warning.Show);
                warningImage.tooltip = warning.Tooltip;
            }
        }
    }
}
