#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{BoxCollider2D, Vector2}"/> that binds <see cref="BoxCollider2D.size"/>.
    /// </summary>
    /// <remarks>
    /// Clamped non-negative on both axes; a non-finite value maps to <c>0</c>.
    /// </remarks>
    [Serializable]
    public class BoxCollider2DSizeBinder : TargetBinder<BoxCollider2D, Vector2>, IVector2Binder
    {
        /// <inheritdoc/>
        protected sealed override Vector2 Property
        {
            get => Target.size;
            set => Target.size = new Vector2(this.SafeClamp(value.x, 0f, float.MaxValue, Target), this.SafeClamp(value.y, 0f, float.MaxValue, Target));
        }

        /// <inheritdoc/>
        public BoxCollider2DSizeBinder(
            BoxCollider2D target,
            IConverter<Vector2, Vector2>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
