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
    /// Clamped non-negative; a non-finite value maps to <c>0</c>.
    /// </remarks>
    [Serializable]
    public class CapsuleColliderHeightBinder : TargetFloatBinder<CapsuleCollider>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => Target.height;
            set => Target.height = this.SafeClamp(value, 0f, float.MaxValue, Target);
        }

        /// <inheritdoc/>
        public CapsuleColliderHeightBinder(
            CapsuleCollider target,
            IConverter<float, float>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
