using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{RectTransform, Vector2}"/> that binds <see cref="RectTransform.offsetMax"/>.
    /// </summary>
    /// <remarks>
    /// The distance between this corner of the element and the anchor it is pinned to, in pixels. It is what
    /// a stretched panel exposes instead of a size: padding that changes with a safe area, a notch, or a
    /// sidebar that slides in.
    /// <para/>
    /// Negative values are ordinary — they push the element past its parent's edge — so only a non-finite
    /// one is refused.
    /// </remarks>
    [AddBinderContextMenu(typeof(RectTransform), serializePropertyNames: "m_SizeDelta")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/RectTransform/RectTransform Binder – OffsetMax")]
    public class RectTransformOffsetMaxMonoBinder : ComponentMonoBinder<RectTransform, Vector2>, IVector2Binder
    {
        /// <inheritdoc/>
        protected sealed override Vector2 Property
        {
            get => CachedComponent.offsetMax;
            set
            {
                if (!this.RequireFinite(value)) return;
                CachedComponent.offsetMax = value;
            }
        }
    }
}
