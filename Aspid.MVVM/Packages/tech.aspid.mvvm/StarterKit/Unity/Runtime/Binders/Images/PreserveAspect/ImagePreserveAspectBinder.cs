#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBoolBinder{Image}"/> that binds <see cref="Image.preserveAspect"/>.
    /// </summary>
    /// <remarks>
    /// Whether the sprite keeps its proportions inside the rect. It matters exactly when the sprite is not known
    /// in advance — an avatar, a downloaded banner, a card art of any shape.
    /// </remarks>
    [Serializable]
    public class ImagePreserveAspectBinder : TargetBoolBinder<Image>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => Target.preserveAspect;
            set => Target.preserveAspect = value;
        }

        /// <inheritdoc/>
        public ImagePreserveAspectBinder(
            Image target,
            bool isInvert = false,
            BindMode mode = BindMode.OneWay)
            : base(target, isInvert, mode) { }
    }
}
