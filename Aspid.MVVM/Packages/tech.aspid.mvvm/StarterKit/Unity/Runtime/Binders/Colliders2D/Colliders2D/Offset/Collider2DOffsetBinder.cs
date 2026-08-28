#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{Collider2D, Vector2}"/> that binds <see cref="Collider2D.offset"/>.
    /// </summary>
    /// <remarks>
    /// Negative offsets are ordinary, so only a non-finite value is refused, leaving the offset unchanged.
    /// </remarks>
    [Serializable]
    public class Collider2DOffsetBinder : TargetBinder<Collider2D, Vector2>, IVector2Binder
    {
        /// <inheritdoc/>
        protected sealed override Vector2 Property
        {
            get => Target.offset;
            set
            {
                if (!this.RequireFinite(value, Target)) return;
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
