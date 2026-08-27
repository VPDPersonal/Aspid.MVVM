#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetFloatBinder{AspectRatioFitter}"/> that binds <see cref="AspectRatioFitter.aspectRatio"/>.
    /// </summary>
    /// <remarks>
    /// Unity clamps the ratio to 0.001..1000, but a NaN value passes that clamp unchanged (every
    /// comparison against NaN is false) and is rejected here instead.
    /// <para/>
    /// While <see cref="AspectRatioFitter.aspectMode"/> is <see cref="AspectRatioFitter.AspectMode.None"/>, the
    /// fitter recomputes the ratio from the element's current rect on every layout pass outside play mode, and a
    /// written value does not survive.
    /// </remarks>
    [Serializable]
    public class AspectRatioFitterAspectRatioBinder : TargetFloatBinder<AspectRatioFitter>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => Target.aspectRatio;
            set
            {
                if (!BinderMath.IsFinite(value)) return;
                Target.aspectRatio = value;
            }
        }

        /// <inheritdoc/>
        public AspectRatioFitterAspectRatioBinder(
            AspectRatioFitter target,
            IConverter<float, float>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
