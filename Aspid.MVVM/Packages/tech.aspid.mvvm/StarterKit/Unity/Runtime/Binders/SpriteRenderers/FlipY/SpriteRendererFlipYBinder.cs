#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBoolBinder{SpriteRenderer}"/> that binds <see cref="SpriteRenderer.flipY"/>.
    /// </summary>
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
            IConverter<bool, bool>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
