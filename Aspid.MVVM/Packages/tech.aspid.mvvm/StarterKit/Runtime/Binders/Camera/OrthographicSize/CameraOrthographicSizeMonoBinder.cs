using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{TComponent}"/> that binds <see cref="Camera.orthographicSize"/>.
    /// </summary>
    /// <remarks>
    /// A negative value mirrors the view and is kept; a non-finite value is refused.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(Camera), serializePropertyNames: "orthographic size")]
    [AddComponentMenu("Aspid/MVVM/Binders/Rendering/Camera Binder – Orthographic Size")]
    public class CameraOrthographicSizeMonoBinder : ComponentFloatMonoBinder<Camera>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.orthographicSize;
            set
            {
                if (this.RequireFinite(value))
                    CachedComponent.orthographicSize = value;
            }
        }
    }
}
