using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets the start and/or end color of each
    /// <see cref="LineRenderer"/>.
    /// </summary>
    [AddBinderContextMenu(typeof(LineRenderer), serializePropertyNames: "colorGradient", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/LineRenderer/LineRenderer Binder – Color EnumGroup")]
    public sealed class LineRendererColorEnumGroupMonoBinder : EnumGroupMonoBinder<LineRenderer, Color>
    {
        [Tooltip("Which end colors the value writes.")]
        [SerializeField] private LineRendererColorMode _colorMode = LineRendererColorMode.StartAndEnd;

        /// <inheritdoc/>
        protected override void SetValue(LineRenderer element, Color value) =>
            element.SetColor(value, _colorMode);
    }
}
