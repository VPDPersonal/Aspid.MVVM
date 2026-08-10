using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentColorMonoBinder<Camera>"/> that binds <see cref="Camera.backgroundColor"/>.
    /// </summary>
    /// <remarks>
    /// What fills the frame where nothing is drawn. Only visible when the camera clears to a solid colour, which
    /// is worth knowing before binding it to a skybox camera and seeing nothing happen.
    /// </remarks>
    [AddBinderContextMenu(typeof(Camera), serializePropertyNames: "m_BackGroundColor")]
    [AddComponentMenu("Aspid/MVVM/Binders/Rendering/Camera Binder – Background Color")]
    public class CameraBackgroundColorMonoBinder : ComponentColorMonoBinder<Camera>
    {
        /// <inheritdoc/>
        protected sealed override Color Property
        {
            get => CachedComponent.backgroundColor;
            set => CachedComponent.backgroundColor = value;
        }
    }
}
