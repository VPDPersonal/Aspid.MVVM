using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent, TValue}"/> that sets the start and/or end color of a
    /// <see cref="LineRenderer"/>.
    /// </summary>
    [AddBinderContextMenu(typeof(LineRenderer), serializePropertyNames: "colorGradient", SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/LineRenderer/LineRenderer Binder – Color Enum")]
    public sealed class LineRendererColorEnumMonoBinder : EnumMonoBinder<LineRenderer, Color>
    {
        [Tooltip("Which end colors the value writes.")]
        [SerializeField] private LineRendererColorMode _colorMode = LineRendererColorMode.StartAndEnd;

        /// <inheritdoc/>
        protected override void SetValue(Color value) =>
            CachedComponent.SetColor(value, _colorMode);
    }
}
