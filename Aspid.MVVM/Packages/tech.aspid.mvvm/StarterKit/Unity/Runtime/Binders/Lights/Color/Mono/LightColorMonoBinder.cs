using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{Light, Color}"/> that binds <see cref="Light.color"/>.
    /// </summary>
    [AddBinderContextMenu(typeof(Light), serializePropertyNames: "m_Color")]
    [AddComponentMenu("Aspid/MVVM/Binders/Rendering/Light Binder – Color")]
    public class LightColorMonoBinder : ComponentMonoBinder<Light, Color>, IColorBinder
    {
        /// <inheritdoc/>
        protected sealed override Color Property
        {
            get => CachedComponent.color;
            set => CachedComponent.color = value;
        }
    }
}
