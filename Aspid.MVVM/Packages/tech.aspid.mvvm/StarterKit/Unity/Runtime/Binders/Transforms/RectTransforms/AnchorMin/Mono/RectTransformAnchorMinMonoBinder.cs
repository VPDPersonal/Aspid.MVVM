using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentVector2MonoBinder{RectTransform}"/> that binds <see cref="RectTransform.anchorMin"/>.
    /// </summary>
    /// <remarks>
    /// Where the element's lower-left corner is pinned inside its parent, as a fraction. Moving the anchors is how a
    /// panel switches between hugging one edge and stretching across the whole parent — a layout decision a ViewModel
    /// makes when the screen or the mode changes.
    /// <para/>
    /// Values outside 0..1 are legal — that is how an element is stretched past its parent — so only a
    /// non-finite one is refused: the rect is computed from these numbers and one <c>NaN</c> takes the element
    /// off the screen.
    /// </remarks>
    [AddBinderContextMenu(typeof(RectTransform), serializePropertyNames: "m_AnchorMin")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/RectTransform/RectTransform Binder – AnchorMin")]
    public class RectTransformAnchorMinMonoBinder : ComponentVector2MonoBinder<RectTransform>
    {
        /// <inheritdoc/>
        protected sealed override Vector2 Property
        {
            get => CachedComponent.anchorMin;
            set
            {
                if (!BinderMath.IsFinite(value.x) || !BinderMath.IsFinite(value.y)) return;
                CachedComponent.anchorMin = value;
            }
        }
    }
}
