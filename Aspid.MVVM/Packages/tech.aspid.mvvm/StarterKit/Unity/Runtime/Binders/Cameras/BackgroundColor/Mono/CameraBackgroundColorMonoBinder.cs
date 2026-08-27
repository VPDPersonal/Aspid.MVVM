using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentColorMonoBinder{Camera}"/> that binds <see cref="Camera.backgroundColor"/>.
    /// </summary>
    /// <remarks>Only visible when the camera's clear flags are set to solid color.</remarks>
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
