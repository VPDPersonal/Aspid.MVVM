using UnityEngine;
using UnityEngine.Rendering;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent, TProperty}"/> that binds <see cref="Renderer.shadowCastingMode"/>.
    /// </summary>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(Renderer), serializePropertyNames: "m_CastShadows")]
    [AddComponentMenu("Aspid/MVVM/Binders/Renderer/Renderer Binder – Shadow Casting")]
    public class RendererShadowCastingMonoBinder : ComponentMonoBinder<Renderer, ShadowCastingMode>
    {
        /// <inheritdoc/>
        protected sealed override ShadowCastingMode Property
        {
            get => CachedComponent.shadowCastingMode;
            set => CachedComponent.shadowCastingMode = value;
        }
    }
}
