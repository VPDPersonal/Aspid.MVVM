#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{T1, T2}">TargetBinder&lt;Rigidbody, RigidbodyConstraints&gt;</see> that binds
    /// <see cref="Rigidbody.constraints"/>.
    /// </summary>
    [Serializable]
    public class RigidbodyConstraintsBinder : TargetBinder<Rigidbody, RigidbodyConstraints>
    {
        /// <inheritdoc/>
        protected sealed override RigidbodyConstraints Property
        {
            get => Target.constraints;
            set => Target.constraints = value;
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> — the property raises no change event to listen to.</exception>
        public RigidbodyConstraintsBinder(Rigidbody target, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}
