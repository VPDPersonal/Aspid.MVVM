#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{Rigidbody, bool}"/> that binds <see cref="Rigidbody.isKinematic"/>.
    /// </summary>
    [Serializable]
    public class RigidbodyIsKinematicBinder : TargetBinder<Rigidbody, bool>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => Target.isKinematic;
            set => Target.isKinematic = value;
        }

        /// <inheritdoc/>
        public RigidbodyIsKinematicBinder(
            Rigidbody target,
            IConverter<bool, bool>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
