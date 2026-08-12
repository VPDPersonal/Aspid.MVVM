using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder<Camera>"/> that binds <see cref="Camera.fieldOfView"/>.
    /// </summary>
    /// <remarks>
    /// The vertical field of view of a perspective camera, in degrees — the number behind a zoom, a scope or a
    /// dolly-zoom effect, and it had no binder. A non-finite value is dropped rather than written. Unity clamps
    /// the range on its own, so nothing else needs guarding here, but it stores <see cref="float.NaN"/> verbatim —
    /// and a NaN in a rendering number does not fail loudly, it just makes the image wrong in a way that points
    /// nowhere near the ViewModel that produced it.
    /// </remarks>
    [AddBinderContextMenu(typeof(Camera), serializePropertyNames: "field of view")]
    [AddComponentMenu("Aspid/MVVM/Binders/Rendering/Camera Binder – Field Of View")]
    public class CameraFieldOfViewMonoBinder : ComponentFloatMonoBinder<Camera>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.fieldOfView;
            set
            {
                if (!BinderMath.IsFinite(value)) return;
                CachedComponent.fieldOfView = value;
            }
        }
    }
}
