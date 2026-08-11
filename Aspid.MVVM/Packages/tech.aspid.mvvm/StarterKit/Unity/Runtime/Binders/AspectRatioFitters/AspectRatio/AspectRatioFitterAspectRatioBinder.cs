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
    /// The ratio itself — the width of the image the ViewModel just loaded, divided by its height. Unity
    /// clamps the range; a non-finite value is refused here, because Unity's clamp is written as comparisons
    /// and every comparison against NaN is false.
    /// <para/>
    /// Bind <see cref="AspectRatioFitter.aspectMode"/> too, or set it in the Inspector: while it is
    /// <see cref="AspectRatioFitter.AspectMode.None"/>, the fitter recomputes the ratio from the element's current
    /// rect on every layout pass outside play mode, and a written value does not survive.
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
                // Unity сама зажимает соотношение в 0.001..1000, но NaN проходит сквозь её Clamp
                // (любое сравнение с NaN ложно) и обнуляет размер элемента.
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
