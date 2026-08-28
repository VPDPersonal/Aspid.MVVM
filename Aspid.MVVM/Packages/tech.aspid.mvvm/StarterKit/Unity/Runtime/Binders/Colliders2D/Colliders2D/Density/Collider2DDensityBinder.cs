#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetFloatBinder{Collider2D}"/> that binds <see cref="Collider2D.density"/>.
    /// </summary>
    /// <remarks>
    /// Clamped non-negative; a non-finite value maps to <c>0</c>.
    /// <para/>
    /// Unity ignores the write unless the attached <see cref="Rigidbody2D"/> has
    /// <see cref="Rigidbody2D.useAutoMass"/> enabled, silently keeping the previous value.
    /// </remarks>
    [Serializable]
    public class Collider2DDensityBinder : TargetFloatBinder<Collider2D>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => Target.density;
            set => Target.density = this.SafeClamp(value, 0f, float.MaxValue, Target);
        }

        /// <inheritdoc/>
        public Collider2DDensityBinder(
            Collider2D target,
            IConverter<float, float>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
