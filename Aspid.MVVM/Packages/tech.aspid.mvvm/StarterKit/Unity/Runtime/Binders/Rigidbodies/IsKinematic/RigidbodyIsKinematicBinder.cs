#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBoolBinder{Rigidbody}"/> that binds <see cref="Rigidbody.isKinematic"/>.
    /// </summary>
    /// <remarks>
    /// Switches the body between simulated and script-driven — the usual way to hand an object over to an animation
    /// and take it back.
    /// </remarks>
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
            bool isInvert = false,
            BindMode mode = BindMode.OneWay)
            : base(target, isInvert, mode) { }
    }
}
