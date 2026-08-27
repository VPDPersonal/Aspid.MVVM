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
    /// <remarks>The valid values depend on the current <see cref="Image.fillMethod"/> — this indexes that method's origin enum.</remarks>
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
