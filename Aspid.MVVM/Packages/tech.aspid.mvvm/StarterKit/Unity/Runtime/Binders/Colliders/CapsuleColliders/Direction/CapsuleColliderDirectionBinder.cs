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
    /// Clamped to 0–2 (X/Y/Z); Unity silently treats an out-of-range value as 0.
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
