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
    /// How heavy the shape is, when the body computes its mass from its colliders — a crate that fills with
    /// water, a balloon that deflates. Clamped non-negative.
    /// <para/>
    /// Unity <em>ignores</em> the write unless the attached <see cref="Rigidbody2D"/> has
    /// <see cref="Rigidbody2D.useAutoMass"/> enabled — the property keeps its previous value and nothing is logged.
    /// Bind the density only on a body that computes its mass from its colliders.
    /// </remarks>
    [Serializable]
    public class Collider2DDensityBinder : TargetFloatBinder<Collider2D>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => Target.density;
            set => Target.density = BinderMath.SafeClamp(value, 0f, float.MaxValue);
        }

        /// <inheritdoc/>
        public Collider2DDensityBinder(
            Collider2D target,
            IConverter<float, float>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
