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
    /// Half the vertical height the camera sees — the zoom of a 2D or isometric game. Unity does not clamp it, and
    /// a negative value mirrors the view rather than being rejected, so only a non-finite value is dropped.
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
