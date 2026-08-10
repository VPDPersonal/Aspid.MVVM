using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder<Camera>"/> that binds <see cref="Camera.orthographicSize"/>.
    /// </summary>
    /// <remarks>
    /// Half the vertical height the camera sees — the zoom of a 2D or isometric game. Unity does not clamp it, and
    /// a negative value mirrors the view rather than being rejected, so only a non-finite value is dropped.
    /// </remarks>
    [AddBinderContextMenu(typeof(Camera), serializePropertyNames: "orthographic size")]
    [AddComponentMenu("Aspid/MVVM/Binders/Rendering/Camera Binder - Orthographic Size")]
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
