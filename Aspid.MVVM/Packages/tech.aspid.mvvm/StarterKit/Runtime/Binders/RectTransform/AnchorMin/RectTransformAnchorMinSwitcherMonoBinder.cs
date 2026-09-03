using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinder{TComponent, T}"/> that switches <see cref="RectTransform.anchorMin"/>.
    /// </summary>
    /// <remarks>
    /// Values outside 0..1 are legal; only a non-finite value is refused.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(RectTransform), serializePropertyNames: "m_AnchorMin", SubPath = "Switcher")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/RectTransform/RectTransform Binder – AnchorMin Switcher")]
    public sealed class RectTransformAnchorMinSwitcherMonoBinder : SwitcherMonoBinder<RectTransform, Vector2>
    {
        /// <inheritdoc/>
        protected override void SetValue(Vector2 value)
        {
            if (this.RequireFinite(value))
                CachedComponent.anchorMin = value;
        }
    }
}
