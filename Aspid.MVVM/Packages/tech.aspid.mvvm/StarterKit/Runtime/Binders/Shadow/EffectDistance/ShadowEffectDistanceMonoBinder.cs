using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent, TProperty}"/> that binds <see cref="Shadow.effectDistance"/>.
    /// </summary>
    /// <remarks>
    /// Negative offsets are kept; a non-finite value is refused.
    /// </remarks>
    [GenerateSerializableBinder]
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
                if (this.RequireFinite(value))
                    CachedComponent.effectDistance = value;
            }
        }
    }
}
