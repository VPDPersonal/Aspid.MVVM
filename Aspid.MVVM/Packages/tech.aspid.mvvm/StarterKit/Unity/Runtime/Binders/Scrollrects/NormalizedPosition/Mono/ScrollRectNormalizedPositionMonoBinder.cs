using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentVector2MonoBinder{ScrollRect}"/> that binds <see cref="ScrollRect.normalizedPosition"/>.
    /// </summary>
    [AddBinderContextMenu(typeof(ScrollRect), serializePropertyNames: "m_Content")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/ScrollRect/ScrollRect Binder – Normalized Position")]
    public class ScrollRectNormalizedPositionMonoBinder : ComponentVector2MonoBinder<ScrollRect>
    {
        /// <inheritdoc/>
        protected sealed override Vector2 Property
        {
            get => CachedComponent.normalizedPosition;
            set => CachedComponent.normalizedPosition = new Vector2(BinderMath.SafeClamp01(value.x), BinderMath.SafeClamp01(value.y));
        }
    }
}
