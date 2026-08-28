#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{CapsuleCollider2D, Vector2}"/> that binds <see cref="CapsuleCollider2D.size"/>.
    /// </summary>
    /// <remarks>
    /// Clamped non-negative on both axes; a non-finite value maps to <c>0</c>.
    /// </remarks>
    [Serializable]
    public class CapsuleCollider2DSizeBinder : TargetBinder<CapsuleCollider2D, Vector2>, IVector2Binder
    {
        /// <inheritdoc/>
        protected sealed override Vector2 Property
        {
            get => Target.size;
            set => Target.size = new Vector2(this.SafeClamp(value.x, 0f, float.MaxValue, Target), this.SafeClamp(value.y, 0f, float.MaxValue, Target));
        }

        /// <inheritdoc/>
        public CapsuleCollider2DSizeBinder(
            CapsuleCollider2D target,
            IConverter<Vector2, Vector2>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
