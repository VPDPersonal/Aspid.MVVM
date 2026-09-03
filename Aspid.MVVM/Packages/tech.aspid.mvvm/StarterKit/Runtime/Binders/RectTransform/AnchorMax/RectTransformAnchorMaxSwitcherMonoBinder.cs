using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinder{TComponent, T}"/> that switches <see cref="RectTransform.anchorMax"/>.
    /// </summary>
    /// <remarks>
    /// Values outside 0..1 are legal; only a non-finite value is refused.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(RectTransform), serializePropertyNames: "m_AnchorMax", SubPath = "Switcher")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/RectTransform/RectTransform Binder – AnchorMax Switcher")]
    public sealed class RectTransformAnchorMaxSwitcherMonoBinder : SwitcherMonoBinder<RectTransform, Vector2>
    {
        /// <inheritdoc/>
        protected override void SetValue(Vector2 value)
        {
            if (this.RequireFinite(value))
                CachedComponent.anchorMax = value;
        }
    }
}
