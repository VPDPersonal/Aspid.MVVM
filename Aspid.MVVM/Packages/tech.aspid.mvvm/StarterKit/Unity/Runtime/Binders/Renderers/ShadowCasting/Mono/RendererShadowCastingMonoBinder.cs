using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{T1, T2}">ComponentMonoBinder&lt;Renderer, UnityEngine.Rendering.ShadowCastingMode&gt;</see> that binds
    /// <see cref="Renderer.shadowCastingMode"/>.
    /// </summary>
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
