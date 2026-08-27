#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetVector2Binder{SpriteRenderer}"/> that binds <see cref="SpriteRenderer.size"/>.
    /// </summary>
    /// <remarks>
    /// Ignored by Unity unless <see cref="SpriteRenderer.drawMode"/> is <see cref="SpriteDrawMode.Sliced"/> or
    /// <see cref="SpriteDrawMode.Tiled"/>. Negative and non-finite values are clamped to zero.
    /// </remarks>
    [Serializable]
    public class SpriteRendererSizeBinder : TargetVector2Binder<SpriteRenderer>
    {
        /// <inheritdoc/>
        protected sealed override Vector2 Property
        {
            get => Target.size;
            set => Target.size = new Vector2(BinderMath.SafeClamp(value.x, 0f, float.MaxValue), BinderMath.SafeClamp(value.y, 0f, float.MaxValue));
        }

        /// <inheritdoc/>
        public SpriteRendererSizeBinder(
            SpriteRenderer target,
            IConverter<Vector2, Vector2>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
