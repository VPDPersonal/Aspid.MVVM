#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBoolBinder{Collider2D}"/> that binds <see cref="Collider2D.isTrigger"/>.
    /// </summary>
    [Serializable]
    public class Collider2DIsTriggerBinder : TargetBoolBinder<Collider2D>
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
            bool isInvert = false,
            BindMode mode = BindMode.OneWay)
            : base(target, isInvert, mode) { }
    }
}
