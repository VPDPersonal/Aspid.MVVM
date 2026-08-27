using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{Camera}"/> that binds <see cref="Camera.orthographicSize"/>.
    /// </summary>
    /// <remarks>
    /// Unity does not clamp this value; a negative value mirrors the view instead of being rejected. Non-finite
    /// values are dropped.
    /// </remarks>
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
                if (!BinderMath.IsFinite(value)) return;
                CachedComponent.orthographicSize = value;
            }
        }
    }
}
