#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBoolBinder{Rigidbody2D}"/> that binds <see cref="Rigidbody2D.simulated"/>.
    /// </summary>
    /// <remarks>
    /// Takes the body out of the simulation together with its colliders — cheaper than disabling the object when
    /// only physics should pause.
    /// </remarks>
    [Serializable]
    public class Rigidbody2DSimulatedBinder : TargetBoolBinder<Rigidbody2D>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => Target.simulated;
            set => Target.simulated = value;
        }

        /// <inheritdoc/>
        public Rigidbody2DSimulatedBinder(
            Rigidbody2D target,
            bool isInvert = false,
            BindMode mode = BindMode.OneWay)
            : base(target, isInvert, mode) { }
    }
}
