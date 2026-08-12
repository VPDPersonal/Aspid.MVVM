using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentColorMonoBinder{Light}"/> that binds <see cref="Light.color"/>.
    /// </summary>
    /// <remarks>
    /// Lighting had no binders at all. Tinting a lamp from the ViewModel — a warning light going red, a torch
    /// guttering — meant reaching for the component by hand.
    /// </remarks>
    [AddBinderContextMenu(typeof(Light), serializePropertyNames: "m_Color")]
    [AddComponentMenu("Aspid/MVVM/Binders/Rendering/Light Binder – Color")]
    public class LightColorMonoBinder : ComponentColorMonoBinder<Light>
    {
        /// <inheritdoc/>
        protected sealed override Color Property
        {
            get => CachedComponent.color;
            set => CachedComponent.color = value;
        }
    }
}
