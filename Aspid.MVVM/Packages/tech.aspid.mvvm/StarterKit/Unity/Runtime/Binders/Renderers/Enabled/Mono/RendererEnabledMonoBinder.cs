using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentBoolMonoBinder{Renderer}"/> that binds the <see cref="Renderer.enabled"/> property.
    /// </summary>
    /// <remarks>
    /// A <see cref="Renderer"/> is a <see cref="Component"/> and not a <see cref="Behaviour"/>, so the behaviour binders cannot take one — this is the equivalent for it.
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established, the current value is sent back
    /// to the ViewModel. Supports optional value inversion.
    /// </remarks>
    [AddBinderContextMenu(typeof(Renderer), serializePropertyNames: "m_Enabled")]
    [AddComponentMenu("Aspid/MVVM/Binders/Renderer/Renderer Binder – Enabled")]
    public class RendererEnabledMonoBinder : ComponentBoolMonoBinder<Renderer>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => CachedComponent.enabled;
            set => CachedComponent.enabled = value;
        }
    }
}
