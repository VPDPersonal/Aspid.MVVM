using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent, TProperty}"/> that binds <see cref="Renderer.enabled"/>.
    /// </summary>
    /// <remarks>
    /// <see cref="Renderer"/> is not a <see cref="Behaviour"/>, so the Behaviour binders cannot take it.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(Renderer), serializePropertyNames: "m_Enabled")]
    [AddComponentMenu("Aspid/MVVM/Binders/Renderer/Renderer Binder – Enabled")]
    public class RendererEnabledMonoBinder : ComponentMonoBinder<Renderer, bool>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => CachedComponent.enabled;
            set => CachedComponent.enabled = value;
        }
    }
}
