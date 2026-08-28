#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{SpriteRenderer, bool}"/> that binds <see cref="SpriteRenderer.flipY"/>.
    /// </summary>
    [Serializable]
    public class SpriteRendererFlipYBinder : TargetBinder<SpriteRenderer, bool>
    {
        /// <inheritdoc/>
        public SpriteRendererFlipYBinder(
            SpriteRenderer target,
            IConverter<bool, bool>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }

        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => Target.flipY;
            set => Target.flipY = value;
        }
    }
}
