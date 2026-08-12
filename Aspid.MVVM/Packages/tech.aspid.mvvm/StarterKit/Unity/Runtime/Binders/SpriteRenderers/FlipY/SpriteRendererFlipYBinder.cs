#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBoolBinder{SpriteRenderer}"/> that binds <see cref="SpriteRenderer.flipY"/>.
    /// </summary>
    /// <remarks>
    /// Mirrors the sprite vertically.
    /// </remarks>
    [Serializable]
    public class SpriteRendererFlipYBinder : TargetBoolBinder<SpriteRenderer>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => Target.flipY;
            set => Target.flipY = value;
        }

        /// <inheritdoc/>
        public SpriteRendererFlipYBinder(
            SpriteRenderer target,
            bool isInvert = false,
            BindMode mode = BindMode.OneWay)
            : base(target, isInvert, mode) { }
    }
}
