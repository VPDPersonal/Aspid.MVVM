using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{Shadow, Vector2}"/> that binds <see cref="Shadow.effectDistance"/>.
    /// </summary>
    /// <remarks>
    /// How far the shadow is offset, or how thick an outline is — <see cref="Outline"/> reads the same
    /// property. Negative offsets are ordinary, so only a non-finite value is refused.
    /// </remarks>
    [AddBinderContextMenu(typeof(Shadow), serializePropertyNames: "m_EffectDistance")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Shadow/Shadow Binder – Effect Distance")]
    public class ShadowEffectDistanceMonoBinder : ComponentMonoBinder<Shadow, Vector2>, IVector2Binder
    {
        /// <inheritdoc/>
        protected sealed override Vector2 Property
        {
            get => CachedComponent.effectDistance;
            set
            {
                if (!this.RequireFinite(value)) return;
                CachedComponent.effectDistance = value;
            }
        }
    }
}
