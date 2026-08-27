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
            IConverter<bool, bool>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
