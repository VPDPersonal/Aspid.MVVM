#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetFloatBinder{LineRenderer}"/> that binds <see cref="LineRenderer.widthMultiplier"/>.
    /// </summary>
    /// <remarks>
    /// Scales the whole width curve, so a line authored with a taper keeps its shape and only gets thicker or
    /// thinner — a laser that charges, a rope under tension, a trail that fades. Clamped non-negative.
    /// </remarks>
    [Serializable]
    public class LineRendererWidthMultiplierBinder : TargetFloatBinder<LineRenderer>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => Target.widthMultiplier;
            set => Target.widthMultiplier = BinderMath.SafeClamp(value, 0f, float.MaxValue);
        }

        /// <inheritdoc/>
        public LineRendererWidthMultiplierBinder(
            LineRenderer target,
            IConverter<float, float>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
