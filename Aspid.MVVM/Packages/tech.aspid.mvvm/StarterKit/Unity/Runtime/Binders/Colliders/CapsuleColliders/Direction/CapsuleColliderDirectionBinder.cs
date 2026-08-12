#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetIntBinder{CapsuleCollider}"/> that binds <see cref="CapsuleCollider.direction"/>.
    /// </summary>
    /// <remarks>
    /// Which axis the capsule stands on: 0 for X, 1 for Y, 2 for Z. A character that lies down changes it, and
    /// so does a projectile that turns. Clamped to the three axes that exist — Unity accepts any integer and
    /// then behaves as if it were zero.
    /// </remarks>
    [Serializable]
    public class CapsuleColliderDirectionBinder : TargetIntBinder<CapsuleCollider>
    {
        /// <inheritdoc/>
        protected sealed override int Property
        {
            get => Target.direction;
            set => Target.direction = Mathf.Clamp(value, 0, 2);
        }

        /// <inheritdoc/>
        public CapsuleColliderDirectionBinder(
            CapsuleCollider target,
            IConverter<int, int>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
