using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentBoolMonoBinder<Camera>"/> that binds <see cref="Camera.orthographic"/>.
    /// </summary>
    /// <remarks>
    /// Switches the camera between perspective and orthographic projection.
    /// </remarks>
    [AddBinderContextMenu(typeof(Camera), serializePropertyNames: "orthographic")]
    [AddComponentMenu("Aspid/MVVM/Binders/Rendering/Camera Binder – Orthographic")]
    public class CameraOrthographicMonoBinder : ComponentBoolMonoBinder<Camera>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => CachedComponent.orthographic;
            set => CachedComponent.orthographic = value;
        }
    }
}
