using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentColorMonoBinder{Shadow}"/> that binds <see cref="Shadow.effectColor"/>.
    /// </summary>
    /// <remarks>
    /// The colour of a shadow or an outline — <see cref="Outline"/> is a <see cref="Shadow"/>, so this binder
    /// takes either. It is how a rarity glow, a team colour or a selection highlight is expressed without a
    /// second graphic, and neither effect could be bound at all.
    /// </remarks>
    [AddBinderContextMenu(typeof(Shadow), serializePropertyNames: "m_EffectColor")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Shadow/Shadow Binder – Effect Color")]
    public class ShadowEffectColorMonoBinder : ComponentColorMonoBinder<Shadow>
    {
        /// <inheritdoc/>
        protected sealed override Color Property
        {
            get => CachedComponent.effectColor;
            set => CachedComponent.effectColor = value;
        }
    }
}
