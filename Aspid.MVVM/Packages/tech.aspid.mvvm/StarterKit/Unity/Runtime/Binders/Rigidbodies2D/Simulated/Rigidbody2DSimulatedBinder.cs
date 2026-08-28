#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{Rigidbody2D, bool}"/> that binds <see cref="Rigidbody2D.simulated"/>.
    /// </summary>
    [Serializable]
    public class Rigidbody2DSimulatedBinder : TargetBinder<Rigidbody2D, bool>
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
            IConverter<bool, bool>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
