#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetFloatBinder<Camera>"/> that binds <see cref="Camera.fieldOfView"/>.
    /// </summary>
    /// <remarks>
    /// The vertical field of view of a perspective camera, in degrees — the number behind a zoom, a scope or a
    /// dolly-zoom effect, and it had no binder. A non-finite value is dropped rather than written. Unity clamps
    /// the range on its own, so nothing else needs guarding here, but it stores <see cref="float.NaN"/> verbatim —
    /// and a NaN in a rendering number does not fail loudly, it just makes the image wrong in a way that points
    /// nowhere near the ViewModel that produced it.
    /// </remarks>
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
