using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent, TProperty}"/> that binds <see cref="Camera.backgroundColor"/>.
    /// </summary>
    /// <remarks>
    /// Visible only with solid-color clear flags.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(Camera), serializePropertyNames: "m_BackGroundColor")]
    [AddComponentMenu("Aspid/MVVM/Binders/Rendering/Camera Binder – Background Color")]
    public class CameraBackgroundColorMonoBinder : ComponentMonoBinder<Camera, Color>, IColorBinder
    {
        /// <inheritdoc/>
        protected sealed override Color Property
        {
            get => CachedComponent.backgroundColor;
            set => CachedComponent.backgroundColor = value;
        }
    }
}
