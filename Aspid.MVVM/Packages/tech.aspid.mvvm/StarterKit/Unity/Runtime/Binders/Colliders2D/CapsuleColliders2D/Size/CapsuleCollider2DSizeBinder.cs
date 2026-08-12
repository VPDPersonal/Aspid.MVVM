#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetVector2Binder{CapsuleCollider2D}"/> that binds <see cref="CapsuleCollider2D.size"/>.
    /// </summary>
    /// <remarks>
    /// Width and height of a 2D capsule — the shape most 2D characters stand on, and the one a crouch
    /// changes. Clamped non-negative on both axes.
    /// </remarks>
    [Serializable]
    public class CapsuleCollider2DSizeBinder : TargetVector2Binder<CapsuleCollider2D>
    {
        /// <inheritdoc/>
        protected sealed override Vector2 Property
        {
            get => Target.size;
            set => Target.size = new Vector2(BinderMath.SafeClamp(value.x, 0f, float.MaxValue), BinderMath.SafeClamp(value.y, 0f, float.MaxValue));
        }

        /// <inheritdoc/>
        public CapsuleCollider2DSizeBinder(
            CapsuleCollider2D target,
            IConverter<Vector2, Vector2>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
