#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{SpriteRenderer, Color}"/> that binds <see cref="SpriteRenderer.color"/>.
    /// </summary>
    /// <remarks>
    /// Tints the sprite directly, without touching the shared material the way the renderer colour binders do.
    /// </remarks>
    [Serializable]
    public class SpriteRendererColorBinder : TargetBinder<SpriteRenderer, Color>, IColorBinder
    {
        /// <inheritdoc/>
        public SpriteRendererColorBinder(
            SpriteRenderer target,
            IConverter<Color, Color>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }

        /// <inheritdoc/>
        protected sealed override Color Property
        {
            get => Target.color;
            set => Target.color = value;
        }
    }
}
