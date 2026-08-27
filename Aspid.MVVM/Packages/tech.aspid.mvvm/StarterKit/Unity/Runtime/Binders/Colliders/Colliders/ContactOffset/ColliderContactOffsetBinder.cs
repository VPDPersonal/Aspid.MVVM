#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetFloatBinder{Collider}"/> that binds <see cref="Collider.contactOffset"/>.
    /// </summary>
    /// <remarks>
    /// Clamped to a small positive minimum instead of zero — Unity rejects a zero contact offset and logs an error.
    /// </remarks>
    [Serializable]
    public class ColliderContactOffsetBinder : TargetFloatBinder<Collider>
    {
        /// <summary>
        /// The smallest offset Unity accepts; it refuses zero and logs an error for it.
        /// </summary>
        private const float MinimumContactOffset = 0.0001f;

        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => Target.contactOffset;
            set => Target.contactOffset = BinderMath.SafeClamp(value, MinimumContactOffset, float.MaxValue);
        }

        /// <inheritdoc/>
        public ColliderContactOffsetBinder(
            Collider target,
            IConverter<float, float>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
