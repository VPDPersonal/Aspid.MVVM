#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder<SpriteRenderer, Sprite>"/> that binds <see cref="SpriteRenderer.sprite"/>.
    /// </summary>
    /// <remarks>
    /// The 2D counterpart of the Image sprite binders, which only ever covered uGUI.
    /// </remarks>
    [Serializable]
    public class SpriteRendererSpriteBinder : TargetBinder<SpriteRenderer, Sprite>
    {
        /// <inheritdoc/>
        protected sealed override Sprite? Property
        {
            get => Target.sprite;
            set => Target.sprite = value;
        }

        /// <inheritdoc/>
        public SpriteRendererSpriteBinder(SpriteRenderer target, BindMode mode = BindMode.OneWay)
            : base(target, mode) { }
    }
}
