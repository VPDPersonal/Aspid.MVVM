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
    /// Clamped non-negative; a non-finite value maps to <c>0</c>.
    /// </remarks>
    [Serializable]
    public class CircleCollider2DRadiusBinder : TargetFloatBinder<CircleCollider2D>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => Target.radius;
            set => Target.radius = this.SafeClamp(value, 0f, float.MaxValue, Target);
        }

        /// <inheritdoc/>
        public CircleCollider2DRadiusBinder(
            CircleCollider2D target,
            IConverter<float, float>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
