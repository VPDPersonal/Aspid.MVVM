using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets <see cref="RectTransform.anchorMax"/> on each element.
    /// </summary>
    /// <remarks>
    /// Values outside 0..1 are legal; only a non-finite value is refused.
    /// </remarks>
    [AddBinderContextMenu(typeof(RectTransform), serializePropertyNames: "m_AnchorMax", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/RectTransform/RectTransform Binder – AnchorMax EnumGroup")]
    public sealed class RectTransformAnchorMaxEnumGroupMonoBinder : EnumGroupMonoBinder<RectTransform, Vector2>
    {
        /// <inheritdoc/>
        protected override void SetValue(RectTransform element, Vector2 value)
        {
            if (this.RequireFinite(value))
                element.anchorMax = value;
        }
    }
}
