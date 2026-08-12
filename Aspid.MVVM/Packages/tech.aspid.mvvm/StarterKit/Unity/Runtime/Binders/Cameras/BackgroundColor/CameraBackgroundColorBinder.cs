#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetColorBinder<Camera>"/> that binds <see cref="Camera.backgroundColor"/>.
    /// </summary>
    /// <remarks>
    /// What fills the frame where nothing is drawn. Only visible when the camera clears to a solid colour, which
    /// is worth knowing before binding it to a skybox camera and seeing nothing happen.
    /// </remarks>
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
