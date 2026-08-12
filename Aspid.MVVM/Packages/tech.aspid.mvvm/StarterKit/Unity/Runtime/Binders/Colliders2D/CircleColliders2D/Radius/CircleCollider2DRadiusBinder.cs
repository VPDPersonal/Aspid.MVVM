#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetFloatBinder{CircleCollider2D}"/> that binds <see cref="CircleCollider2D.radius"/>.
    /// </summary>
    /// <remarks>
    /// The 2D counterpart of <see cref="SphereCollider.radius"/>, which the package already bound — an
    /// explosion radius, a pickup range, a shield that grows. Clamped non-negative.
    /// </remarks>
    [Serializable]
    public class CircleCollider2DRadiusBinder : TargetFloatBinder<CircleCollider2D>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => Target.radius;
            set => Target.radius = BinderMath.SafeClamp(value, 0f, float.MaxValue);
        }

        /// <inheritdoc/>
        public CircleCollider2DRadiusBinder(
            CircleCollider2D target,
            IConverter<float, float>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
