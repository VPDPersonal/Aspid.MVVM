using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent, TValue}"/> that sets <see cref="RectTransform.anchorMax"/>.
    /// </summary>
    /// <remarks>
    /// Values outside 0..1 are legal; only a non-finite value is refused.
    /// </remarks>
    [AddBinderContextMenu(typeof(RectTransform), serializePropertyNames: "m_AnchorMax", SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/RectTransform/RectTransform Binder – AnchorMax Enum")]
    public sealed class RectTransformAnchorMaxEnumMonoBinder : EnumMonoBinder<RectTransform, Vector2>
    {
        /// <inheritdoc/>
        protected override void SetValue(Vector2 value)
        {
            if (this.RequireFinite(value))
                CachedComponent.anchorMax = value;
        }
    }
}
