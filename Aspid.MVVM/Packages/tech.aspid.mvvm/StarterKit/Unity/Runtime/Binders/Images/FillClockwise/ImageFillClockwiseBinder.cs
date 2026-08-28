#nullable enable
using System;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{Image, bool}"/> that binds <see cref="Image.fillClockwise"/>.
    /// </summary>
    [Serializable]
    public class ImageFillClockwiseBinder : TargetBinder<Image, bool>
    {
        /// <inheritdoc/>
        public ImageFillClockwiseBinder(
            Image target,
            IConverter<bool, bool>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }

        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => Target.fillClockwise;
            set => Target.fillClockwise = value;
        }
    }
}
