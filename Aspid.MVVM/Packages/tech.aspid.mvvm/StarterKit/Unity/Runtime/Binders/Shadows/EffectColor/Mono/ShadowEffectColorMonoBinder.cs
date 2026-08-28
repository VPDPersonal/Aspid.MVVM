using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{Shadow, Color}"/> that binds <see cref="Shadow.effectColor"/>.
    /// </summary>
    /// <remarks><see cref="Outline"/> is a <see cref="Shadow"/>, so this binder targets either.</remarks>
    [AddBinderContextMenu(typeof(Shadow), serializePropertyNames: "m_EffectColor")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Shadow/Shadow Binder – Effect Color")]
    public class ShadowEffectColorMonoBinder : ComponentMonoBinder<Shadow, Color>, IColorBinder
    {
        /// <inheritdoc/>
        protected sealed override Color Property
        {
            get => CachedComponent.effectColor;
            set => CachedComponent.effectColor = value;
        }
    }
}
