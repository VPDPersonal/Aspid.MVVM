#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{Camera, bool}"/> that binds <see cref="Camera.orthographic"/>.
    /// </summary>
    [Serializable]
    public class CameraOrthographicBinder : TargetBinder<Camera, bool>
    {
        /// <inheritdoc/>
        public CameraOrthographicBinder(
            Camera target,
            IConverter<bool, bool>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }

        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => Target.orthographic;
            set => Target.orthographic = value;
        }
    }
}
