using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumColorMonoBinder{LineRenderer}"/> that sets the <see cref="LineRenderer.startColor"/> and <see cref="LineRenderer.endColor"/>
    /// color to a value resolved from the bound enum ViewModel value depending on the configured <see cref="LineRendererColorMode"/>.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/LineRenderer/LineRenderer Binder – Color Enum")]
    [AddBinderContextMenu(typeof(LineRenderer), serializePropertyNames: "colorGradient", SubPath = "Enum")]
    public sealed class LineRendererColorEnumMonoBinder : EnumColorMonoBinder<LineRenderer>
    {
        [Tooltip("The color endpoint(s) to set when a value is applied.")]
        [SerializeField] private LineRendererColorMode _colorMode = LineRendererColorMode.StartAndEnd;

        /// <inheritdoc/>
        protected override void SetValue(Color value) =>
            CachedComponent.SetColor(value, _colorMode);
    }
}