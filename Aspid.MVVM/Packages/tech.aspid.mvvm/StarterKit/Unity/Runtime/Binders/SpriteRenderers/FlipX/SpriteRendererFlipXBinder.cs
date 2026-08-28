#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{SpriteRenderer, bool}"/> that binds <see cref="SpriteRenderer.flipX"/>.
    /// </summary>
    [Serializable]
    public class SpriteRendererFlipXBinder : TargetBinder<SpriteRenderer, bool>
    {
        /// <inheritdoc/>
        public SpriteRendererFlipXBinder(
            SpriteRenderer target,
            IConverter<bool, bool>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }

        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => Target.flipX;
            set => Target.flipX = value;
        }
    }
}
