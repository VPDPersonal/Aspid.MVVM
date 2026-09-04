using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent, TProperty}"/> that binds the start and/or end color of a
    /// <see cref="LineRenderer"/>.
    /// </summary>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(LineRenderer), serializePropertyNames: "colorGradient")]
    [AddComponentMenu("Aspid/MVVM/Binders/LineRenderer/LineRenderer Binder – Color")]
    public class LineRendererColorMonoBinder : ComponentMonoBinder<LineRenderer, Color>, IColorBinder
    {
        [Tooltip("Which end colors the value writes.")]
        [SerializeField] private LineRendererColorMode _colorMode = LineRendererColorMode.StartAndEnd;

        /// <inheritdoc/>
        protected sealed override Color Property
        {
            get => CachedComponent.GetColor(_colorMode);
            set => CachedComponent.SetColor(value, _colorMode);
        }
    }
}
