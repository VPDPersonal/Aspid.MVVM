#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetColorBinder{SpriteRenderer}"/> that binds <see cref="SpriteRenderer.color"/>.
    /// </summary>
    /// <remarks>
    /// Tints the sprite directly, without touching the shared material the way the renderer colour binders do.
    /// </remarks>
    [Serializable]
    public class SpriteRendererColorBinder : TargetColorBinder<SpriteRenderer>
    {
        /// <inheritdoc/>
        protected sealed override Color Property
        {
            get => Target.color;
            set => Target.color = value;
        }

        /// <inheritdoc/>
        public SpriteRendererColorBinder(
            SpriteRenderer target,
            IConverter<UnityEngine.Color, UnityEngine.Color>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
