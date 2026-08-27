using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{Camera}"/> that binds <see cref="Camera.fieldOfView"/>.
    /// </summary>
    /// <remarks>Non-finite values are dropped instead of being written.</remarks>
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
