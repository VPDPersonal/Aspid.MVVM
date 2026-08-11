#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetFloatBinder{CapsuleCollider}"/> that binds <see cref="CapsuleCollider.height"/>.
    /// </summary>
    /// <remarks>
    /// The domain had the capsule's radius and not its height, which is the half of it a crouch, a stretch or a
    /// growing character changes. Clamped non-negative: a negative height leaves the collider inverted, and a
    /// non-finite one lands on zero rather than reaching the physics engine.
    /// </remarks>
    [Serializable]
    public class CapsuleColliderHeightBinder : TargetFloatBinder<CapsuleCollider>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => Target.height;
            set => Target.height = BinderMath.SafeClamp(value, 0f, float.MaxValue);
        }

        /// <inheritdoc/>
        public CapsuleColliderHeightBinder(
            CapsuleCollider target,
            IConverter<float, float>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
