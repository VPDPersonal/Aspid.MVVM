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
