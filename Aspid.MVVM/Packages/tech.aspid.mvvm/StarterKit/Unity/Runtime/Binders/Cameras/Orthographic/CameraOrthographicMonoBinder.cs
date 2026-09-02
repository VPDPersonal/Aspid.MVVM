using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent,TProperty}"/> that binds <see cref="Camera.orthographic"/>.
    /// </summary>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(Camera), serializePropertyNames: "orthographic")]
    [AddComponentMenu("Aspid/MVVM/Binders/Rendering/Camera Binder – Orthographic")]
    public class CameraOrthographicMonoBinder : ComponentMonoBinder<Camera, bool>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => CachedComponent.orthographic;
            set => CachedComponent.orthographic = value;
        }
    }
}
