using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentColorMonoBinder{LineRenderer}"/> that sets the <see cref="LineRenderer.startColor"/> and <see cref="LineRenderer.endColor"/>
    /// color depending on the configured <see cref="LineRendererColorMode"/>.
    /// </summary>
    /// <remarks>
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established, the current color value
    /// is sent back to the ViewModel.
    /// </remarks>
    [AddComponentMenu("Aspid/MVVM/Binders/LineRenderer/LineRenderer Binder – Color")]
    [AddBinderContextMenu(typeof(LineRenderer), serializePropertyNames: "colorGradient")]
    public class LineRendererColorMonoBinder : ComponentColorMonoBinder<LineRenderer>
    {
        [Tooltip("The color endpoint(s) to set when a value arrives from the ViewModel.")]
        [SerializeField] private LineRendererColorMode _colorMode = LineRendererColorMode.StartAndEnd;

        /// <inheritdoc/>
        protected sealed override Color Property
        {
            get => CachedComponent.GetColor(_colorMode);
            set => CachedComponent.SetColor(value, _colorMode);
        }
    }
}