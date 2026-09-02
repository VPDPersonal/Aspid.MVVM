using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{RectTransform, Vector2}"/> that binds <see cref="RectTransform.offsetMin"/>.
    /// </summary>
    /// <remarks>
    /// Negative values are ordinary — they push the element past its parent's edge — so only a non-finite
    /// one is refused.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(RectTransform), serializePropertyNames: "m_SizeDelta")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/RectTransform/RectTransform Binder – OffsetMin")]
    public class RectTransformOffsetMinMonoBinder : ComponentMonoBinder<RectTransform, Vector2>, IVector2Binder
    {
        /// <inheritdoc/>
        protected sealed override Vector2 Property
        {
            get => CachedComponent.offsetMin;
            set
            {
                if (!this.RequireFinite(value)) return;
                CachedComponent.offsetMin = value;
            }
        }
    }
}
