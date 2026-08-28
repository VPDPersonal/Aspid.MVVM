#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{SpriteRenderer, Sprite}"/> that binds <see cref="SpriteRenderer.sprite"/>.
    /// </summary>
    [Serializable]
    public class SpriteRendererSpriteBinder : TargetBinder<SpriteRenderer, Sprite>
    {
        /// <inheritdoc/>
        public SpriteRendererSpriteBinder(SpriteRenderer target, IConverter<Sprite, Sprite>? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }

        /// <inheritdoc/>
        protected sealed override Sprite? Property
        {
            get => Target.sprite;
            set => Target.sprite = value;
        }
    }
}
