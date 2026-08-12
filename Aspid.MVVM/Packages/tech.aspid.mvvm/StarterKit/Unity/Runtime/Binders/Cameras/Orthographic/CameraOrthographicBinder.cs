#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBoolBinder<Camera>"/> that binds <see cref="Camera.orthographic"/>.
    /// </summary>
    /// <remarks>
    /// Switches the camera between perspective and orthographic projection.
    /// </remarks>
    [Serializable]
    public class CameraOrthographicBinder : TargetBoolBinder<Camera>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => Target.orthographic;
            set => Target.orthographic = value;
        }

        /// <inheritdoc/>
        public CameraOrthographicBinder(
            Camera target,
            bool isInvert = false,
            BindMode mode = BindMode.OneWay)
            : base(target, isInvert, mode) { }
    }
}
