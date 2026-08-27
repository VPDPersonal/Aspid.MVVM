#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetFloatBinder{Camera}"/> that binds <see cref="Camera.fieldOfView"/>.
    /// </summary>
    /// <remarks>Non-finite values are dropped instead of being written.</remarks>
    [Serializable]
    public class CameraFieldOfViewBinder : TargetFloatBinder<Camera>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => Target.fieldOfView;
            set
            {
                if (!BinderMath.IsFinite(value)) return;
                Target.fieldOfView = value;
            }
        }

        /// <inheritdoc/>
        public CameraFieldOfViewBinder(
            Camera target,
            IConverter<float, float>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
