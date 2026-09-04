using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinder{TComponent, T}"/> that switches
    /// <see cref="HorizontalOrVerticalLayoutGroup.spacing"/>.
    /// </summary>
    /// <remarks>
    /// A non-finite value is refused.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(HorizontalOrVerticalLayoutGroup), serializePropertyNames: "m_Spacing", SubPath = "Switcher")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/HorizontalOrVerticalLayoutGroup/HorizontalOrVerticalLayoutGroup Binder – Spacing Switcher")]
    public sealed class HorizontalOrVerticalLayoutGroupSpacingSwitcherMonoBinder
        : SwitcherMonoBinder<HorizontalOrVerticalLayoutGroup, float>
    {
        /// <inheritdoc/>
        protected override void SetValue(float value)
        {
            if (this.RequireFinite(value))
                CachedComponent.spacing = value;
        }
    }
}
