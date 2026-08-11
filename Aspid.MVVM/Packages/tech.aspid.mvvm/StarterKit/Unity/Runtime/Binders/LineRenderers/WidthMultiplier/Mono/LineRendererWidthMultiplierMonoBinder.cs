using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{LineRenderer}"/> that binds <see cref="LineRenderer.widthMultiplier"/>.
    /// </summary>
    /// <remarks>
    /// Scales the whole width curve, so a line authored with a taper keeps its shape and only gets thicker or
    /// thinner — a laser that charges, a rope under tension, a trail that fades. Clamped non-negative.
    /// </remarks>
    [AddBinderContextMenu(typeof(LineRenderer))]
    [AddComponentMenu("Aspid/MVVM/Binders/LineRenderer/LineRenderer Binder – Width Multiplier")]
    public class LineRendererWidthMultiplierMonoBinder : ComponentFloatMonoBinder<LineRenderer>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.widthMultiplier;
            set => CachedComponent.widthMultiplier = BinderMath.SafeClamp(value, 0f, float.MaxValue);
        }
    }
}
