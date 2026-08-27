#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBoolBinder{Rigidbody}"/> that binds <see cref="Rigidbody.useGravity"/>.
    /// </summary>
    [Serializable]
    public class RigidbodyUseGravityBinder : TargetBoolBinder<Rigidbody>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => Target.useGravity;
            set => Target.useGravity = value;
        }

        /// <inheritdoc/>
        public RigidbodyUseGravityBinder(
            Rigidbody target,
            IConverter<bool, bool>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
