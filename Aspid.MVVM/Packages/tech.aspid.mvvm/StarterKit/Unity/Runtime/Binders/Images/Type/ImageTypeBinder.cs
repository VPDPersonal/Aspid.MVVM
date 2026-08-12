#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{T1, T2}">TargetBinder&lt;Image, Image.Type&gt;</see> that binds
    /// <see cref="Image.type"/>.
    /// </summary>
    /// <remarks>
    /// Whether the sprite is drawn simple, sliced, tiled or filled. A bar that switches between a fill and a
    /// plain icon changes this, and so does a panel that must stop stretching its border.
    /// </remarks>
    [Serializable]
    public class ImageTypeBinder : TargetBinder<Image, Image.Type>
    {
        /// <inheritdoc/>
        protected sealed override Image.Type Property
        {
            get => Target.type;
            set => Target.type = value;
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> — the property raises no change event to listen to.</exception>
        public ImageTypeBinder(Image target, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}
