#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBoolBinder{Image}"/> that binds <see cref="Image.fillClockwise"/>.
    /// </summary>
    [Serializable]
    public class ImageFillClockwiseBinder : TargetBoolBinder<Image>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => Target.fillClockwise;
            set => Target.fillClockwise = value;
        }

        /// <inheritdoc/>
        public ImageFillClockwiseBinder(
            Image target,
            bool isInvert = false,
            BindMode mode = BindMode.OneWay)
            : base(target, isInvert, mode) { }
    }
}
