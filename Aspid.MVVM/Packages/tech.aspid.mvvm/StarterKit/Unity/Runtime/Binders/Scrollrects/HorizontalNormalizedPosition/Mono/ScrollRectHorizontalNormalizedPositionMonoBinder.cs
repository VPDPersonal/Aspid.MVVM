using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{ScrollRect}"/> that binds <see cref="ScrollRect.horizontalNormalizedPosition"/>.
    /// </summary>
    [AddBinderContextMenu(typeof(ScrollRect), serializePropertyNames: "m_Content")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/ScrollRect/ScrollRect Binder – Horizontal Scroll")]
    public class ScrollRectHorizontalNormalizedPositionMonoBinder : ComponentFloatMonoBinder<ScrollRect>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.horizontalNormalizedPosition;
            set => CachedComponent.horizontalNormalizedPosition = BinderMath.SafeClamp01(value);
        }
    }
}
