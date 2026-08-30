using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{RectTransform, Vector2}"/> that binds <see cref="RectTransform.anchorMin"/>.
    /// </summary>
    /// <remarks>
    /// Values outside 0..1 are legal; only a non-finite value is refused, since the rect is computed from
    /// these numbers and a <c>NaN</c> takes the element off the screen.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(RectTransform), serializePropertyNames: "m_AnchorMin")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/RectTransform/RectTransform Binder – AnchorMin")]
    public class RectTransformAnchorMinMonoBinder : ComponentMonoBinder<RectTransform, Vector2>, IVector2Binder
    {
        /// <inheritdoc/>
        protected sealed override Vector2 Property
        {
            get => CachedComponent.anchorMin;
            set
            {
                if (!this.RequireFinite(value)) return;
                CachedComponent.anchorMin = value;
            }
        }
    }
}
