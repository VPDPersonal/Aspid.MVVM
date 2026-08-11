using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{T1, T2}">ComponentMonoBinder&lt;Renderer, UnityEngine.Rendering.ShadowCastingMode&gt;</see> that binds
    /// <see cref="Renderer.shadowCastingMode"/>.
    /// </summary>
    /// <remarks>
    /// Whether the renderer casts a shadow, and whether it casts one when invisible. It is a quality setting as
    /// much as a look: turning shadows off per object is the cheapest way to buy frames back on a weak device.
    /// </remarks>
    [AddBinderContextMenu(typeof(Renderer), serializePropertyNames: "m_CastShadows")]
    [AddComponentMenu("Aspid/MVVM/Binders/Renderer/Renderer Binder – Shadow Casting")]
    public class RendererShadowCastingMonoBinder : ComponentMonoBinder<Renderer, UnityEngine.Rendering.ShadowCastingMode>
    {
        /// <inheritdoc/>
        protected sealed override UnityEngine.Rendering.ShadowCastingMode Property
        {
            get => CachedComponent.shadowCastingMode;
            set => CachedComponent.shadowCastingMode = value;
        }
    }
}
