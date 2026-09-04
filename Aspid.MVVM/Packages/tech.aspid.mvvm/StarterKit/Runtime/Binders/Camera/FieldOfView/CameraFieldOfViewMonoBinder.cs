using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{TComponent}"/> that binds <see cref="Camera.fieldOfView"/>.
    /// </summary>
    /// <remarks>
    /// A non-finite value is refused.
    /// </remarks>
    [GenerateSerializableBinder]
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
                if (this.RequireFinite(value))
                    CachedComponent.fieldOfView = value;
            }
        }
    }
}
