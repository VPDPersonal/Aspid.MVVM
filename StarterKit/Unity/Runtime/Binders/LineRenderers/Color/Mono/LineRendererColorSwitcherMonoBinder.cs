using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherColorMonoBinder{LineRenderer}"/> that switches the <see cref="LineRenderer.startColor"/> and <see cref="LineRenderer.endColor"/>
    /// color between two values based on the bound boolean ViewModel value depending on the configured <see cref="LineRendererColorMode"/>.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/LineRenderer/LineRenderer Binder – Color Switcher")]
    [AddBinderContextMenu(typeof(LineRenderer), serializePropertyNames: "colorGradient", SubPath = "Switcher")]
    public sealed class LineRendererColorSwitcherMonoBinder : SwitcherColorMonoBinder<LineRenderer>
    {
        [Tooltip("The color endpoint(s) to set when a value is applied.")]
        [SerializeField] private LineRendererColorMode _colorMode = LineRendererColorMode.StartAndEnd;

        /// <inheritdoc/>
        protected override void SetValue(Color value) =>
            CachedComponent.SetColor(value, _colorMode);
    }
}