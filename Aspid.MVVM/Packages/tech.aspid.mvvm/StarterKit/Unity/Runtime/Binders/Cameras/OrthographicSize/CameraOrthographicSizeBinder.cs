#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetFloatBinder{Camera}"/> that binds <see cref="Camera.orthographicSize"/>.
    /// </summary>
    /// <remarks>
    /// Unity does not clamp this value; a negative value mirrors the view instead of being rejected. Non-finite
    /// values are dropped.
    /// </remarks>
    [Serializable]
    public class CameraOrthographicSizeBinder : TargetFloatBinder<Camera>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => Target.orthographicSize;
            set
            {
                if (!BinderMath.IsFinite(value)) return;
                Target.orthographicSize = value;
            }
        }

        /// <inheritdoc/>
        public CameraOrthographicSizeBinder(
            Camera target,
            IConverter<float, float>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
