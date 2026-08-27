#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBoolBinder{Rigidbody}"/> that binds <see cref="Rigidbody.isKinematic"/>.
    /// </summary>
    [Serializable]
    public class RigidbodyIsKinematicBinder : TargetBoolBinder<Rigidbody>
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
