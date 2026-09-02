using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{LineRenderer}"/> that binds <see cref="LineRenderer.widthMultiplier"/>.
    /// </summary>
    /// <remarks>
    /// Clamped non-negative.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(LineRenderer))]
    [AddComponentMenu("Aspid/MVVM/Binders/LineRenderer/LineRenderer Binder – Width Multiplier")]
    public class LineRendererWidthMultiplierMonoBinder : ComponentFloatMonoBinder<LineRenderer>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.widthMultiplier;
            set => CachedComponent.widthMultiplier = this.SafeClamp(value, 0f, float.MaxValue);
        }
    }
}
