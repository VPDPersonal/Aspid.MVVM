#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetVector2Binder{Collider2D}"/> that binds <see cref="Collider2D.offset"/>.
    /// </summary>
    /// <remarks>
    /// Negative offsets are ordinary, so only a non-finite value is refused, leaving the offset unchanged.
    /// </remarks>
    [Serializable]
    public class Collider2DOffsetBinder : TargetVector2Binder<Collider2D>
    {
        /// <inheritdoc/>
        protected sealed override Vector2 Property
        {
            get => Target.offset;
            set
            {
                if (!BinderMath.IsFinite(value.x) || !BinderMath.IsFinite(value.y)) return;
                Target.offset = value;
            }
        }

        /// <inheritdoc/>
        public Collider2DOffsetBinder(
            Collider2D target,
            IConverter<Vector2, Vector2>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
