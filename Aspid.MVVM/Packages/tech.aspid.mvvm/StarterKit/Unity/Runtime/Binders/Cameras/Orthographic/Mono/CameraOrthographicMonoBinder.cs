using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentBoolMonoBinder{Camera}"/> that binds <see cref="Camera.orthographic"/>.
    /// </summary>
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
