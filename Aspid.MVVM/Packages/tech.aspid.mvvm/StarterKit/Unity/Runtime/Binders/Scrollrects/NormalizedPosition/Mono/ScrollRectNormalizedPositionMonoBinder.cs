using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentVector2MonoBinder{ScrollRect}"/> that binds <see cref="ScrollRect.normalizedPosition"/>.
    /// </summary>
    /// <remarks>
    /// Both axes at once, for the case the two scalar binders cannot express: restoring a remembered
    /// position, or scrolling a grid back to a corner. Each component is clamped to 0..1 separately —
    /// Unity clamps them silently anyway, and a non-finite one would leave the content nowhere.
    /// </remarks>
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
