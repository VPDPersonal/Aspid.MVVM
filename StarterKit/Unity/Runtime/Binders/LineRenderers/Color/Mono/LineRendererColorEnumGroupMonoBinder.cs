using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupColorMonoBinder{LineRenderer}"/> that sets the color of each <see cref="LineRenderer.startColor"/> and <see cref="LineRenderer.endColor"/>
    /// element to the resolved value depending on the configured <see cref="LineRendererColorMode"/>.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/LineRenderer/LineRenderer Binder – Color EnumGroup")]
    [AddBinderContextMenu(typeof(LineRenderer), serializePropertyNames: "colorGradient", SubPath = "EnumGroup")]
    public sealed class LineRendererColorEnumGroupMonoBinder : EnumGroupColorMonoBinder<LineRenderer>
    {
        [Tooltip("The color endpoint(s) to set when a value is applied to each element.")]
        [SerializeField] private LineRendererColorMode _colorMode = LineRendererColorMode.StartAndEnd;

        /// <inheritdoc/>
        protected override void SetValue(LineRenderer element, Color value) =>
            element.SetColor(value, _colorMode);
    }
}