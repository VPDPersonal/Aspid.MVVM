#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{T1, T2}">TargetBinder&lt;Rigidbody2D, RigidbodyType2D&gt;</see> that binds
    /// <see cref="Rigidbody2D.bodyType"/>.
    /// </summary>
    /// <remarks>
    /// The 2D counterpart of <see cref="Rigidbody.isKinematic"/>, and wider than it:
    /// <see cref="RigidbodyType2D.Static"/> takes the body out of the simulation entirely, which is what a platform
    /// that stops moving wants. <see cref="Rigidbody2D.simulated"/> answers a different question — whether the body
    /// is simulated at all — so both binders exist side by side.
    /// </remarks>
    [Serializable]
    public class Rigidbody2DBodyTypeBinder : TargetBinder<Rigidbody2D, RigidbodyType2D>
    {
        /// <inheritdoc/>
        protected sealed override RigidbodyType2D Property
        {
            get => Target.bodyType;
            set => Target.bodyType = value;
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> — the property raises no change event to listen to.</exception>
        public Rigidbody2DBodyTypeBinder(Rigidbody2D target, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}
