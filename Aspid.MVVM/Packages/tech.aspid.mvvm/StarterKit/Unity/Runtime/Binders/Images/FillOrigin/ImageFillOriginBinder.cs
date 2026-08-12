#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetIntBinder{Image}"/> that binds <see cref="Image.fillOrigin"/>.
    /// </summary>
    /// <remarks>
    /// Where a filled image starts filling from, as an index into the origin enum of the current
    /// <see cref="Image.fillMethod"/>. A cooldown that runs from the top and a cast bar that runs from the left
    /// differ only in this number.
    /// </remarks>
    [Serializable]
    public class ImageFillOriginBinder : TargetIntBinder<Image>
    {
        /// <inheritdoc/>
        protected sealed override int Property
        {
            get => Target.fillOrigin;
            set => Target.fillOrigin = value;
        }

        /// <inheritdoc/>
        public ImageFillOriginBinder(
            Image target,
            IConverter<int, int>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
