#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBoolBinder{Camera}"/> that binds <see cref="Camera.orthographic"/>.
    /// </summary>
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
            IConverter<bool, bool>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
