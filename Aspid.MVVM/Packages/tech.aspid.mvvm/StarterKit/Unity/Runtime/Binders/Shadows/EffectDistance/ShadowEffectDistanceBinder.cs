#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetVector2Binder{Shadow}"/> that binds <see cref="Shadow.effectDistance"/>.
    /// </summary>
    /// <remarks>
    /// How far the shadow is offset, or how thick an outline is — <see cref="Outline"/> reads the same
    /// property. Negative offsets are ordinary, so only a non-finite value is refused.
    /// </remarks>
    [Serializable]
    public class ShadowEffectDistanceBinder : TargetVector2Binder<Shadow>
    {
        /// <inheritdoc/>
        protected sealed override Vector2 Property
        {
            get => Target.effectDistance;
            set
            {
                if (!BinderMath.IsFinite(value.x) || !BinderMath.IsFinite(value.y)) return;
                Target.effectDistance = value;
            }
        }

        /// <inheritdoc/>
        public ShadowEffectDistanceBinder(
            Shadow target,
            IConverter<Vector2, Vector2>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
