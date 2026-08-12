#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetFloatBinder{Rigidbody2D}"/> that binds <see cref="Rigidbody2D.gravityScale"/>.
    /// </summary>
    /// <remarks>
    /// How strongly gravity pulls this body, where <c>0</c> suspends it and a negative value inverts it — so
    /// nothing is clamped. Unity refuses a non-finite scale on its own.
    /// </remarks>
    [Serializable]
    public class Rigidbody2DGravityScaleBinder : TargetFloatBinder<Rigidbody2D>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => Target.gravityScale;
            set => Target.gravityScale = value;
        }

        /// <inheritdoc/>
        public Rigidbody2DGravityScaleBinder(
            Rigidbody2D target,
            IConverter<float, float>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
