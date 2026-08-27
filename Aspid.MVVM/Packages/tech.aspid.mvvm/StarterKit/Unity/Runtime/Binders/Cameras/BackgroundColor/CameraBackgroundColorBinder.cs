#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetColorBinder{Camera}"/> that binds <see cref="Camera.backgroundColor"/>.
    /// </summary>
    /// <remarks>Only visible when the camera's clear flags are set to solid color.</remarks>
    [Serializable]
    public class CameraBackgroundColorBinder : TargetColorBinder<Camera>
    {
        /// <inheritdoc/>
        protected sealed override Color Property
        {
            get => Target.backgroundColor;
            set => Target.backgroundColor = value;
        }

        /// <inheritdoc/>
        public CameraBackgroundColorBinder(
            Camera target,
            IConverter<Color, Color>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
