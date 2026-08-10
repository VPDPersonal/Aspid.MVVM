#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBoolBinder<SpriteRenderer>"/> that binds <see cref="SpriteRenderer.flipX"/>.
    /// </summary>
    /// <remarks>
    /// Mirroring a sprite is how a 2D character faces the other way; it needed a scale binder before.
    /// </remarks>
    [Serializable]
    public class SpriteRendererFlipXBinder : TargetBoolBinder<SpriteRenderer>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => Target.flipX;
            set => Target.flipX = value;
        }

        /// <inheritdoc/>
        public SpriteRendererFlipXBinder(
            SpriteRenderer target,
            bool isInvert = false,
            BindMode mode = BindMode.OneWay)
            : base(target, isInvert, mode) { }
    }
}
