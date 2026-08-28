#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{Collider2D, bool}"/> that binds <see cref="Collider2D.isTrigger"/>.
    /// </summary>
    [Serializable]
    public class Collider2DIsTriggerBinder : TargetBinder<Collider2D, bool>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => Target.isTrigger;
            set => Target.isTrigger = value;
        }

        /// <inheritdoc/>
        public Collider2DIsTriggerBinder(
            Collider2D target,
            IConverter<bool, bool>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
