using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinder{TComponent, T}"/> that switches the start and/or end color of a
    /// <see cref="LineRenderer"/>.
    /// </summary>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(LineRenderer), serializePropertyNames: "colorGradient", SubPath = "Switcher")]
    [AddComponentMenu("Aspid/MVVM/Binders/LineRenderer/LineRenderer Binder – Color Switcher")]
    public sealed class LineRendererColorSwitcherMonoBinder : SwitcherMonoBinder<LineRenderer, Color>
    {
        [Tooltip("Which end colors the value writes.")]
        [SerializeField] private LineRendererColorMode _colorMode = LineRendererColorMode.StartAndEnd;

        /// <inheritdoc/>
        protected override void SetValue(Color value) =>
            CachedComponent.SetColor(value, _colorMode);
    }
}
