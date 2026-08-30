using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{RectTransform, Vector2}"/> that binds <see cref="RectTransform.offsetMax"/>.
    /// </summary>
    /// <remarks>
    /// Negative values are ordinary — they push the element past its parent's edge — so only a non-finite
    /// one is refused.
    /// </remarks>
    [GenerateSerializableBinder]
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
