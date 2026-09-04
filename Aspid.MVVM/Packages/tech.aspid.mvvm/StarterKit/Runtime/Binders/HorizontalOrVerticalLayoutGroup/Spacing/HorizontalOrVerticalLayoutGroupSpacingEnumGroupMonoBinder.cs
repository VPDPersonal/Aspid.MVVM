using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets
    /// <see cref="HorizontalOrVerticalLayoutGroup.spacing"/> on each element.
    /// </summary>
    /// <remarks>
    /// A non-finite value is refused.
    /// </remarks>
    [AddBinderContextMenu(typeof(HorizontalOrVerticalLayoutGroup), serializePropertyNames: "m_Spacing", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/HorizontalOrVerticalLayoutGroup/HorizontalOrVerticalLayoutGroup Binder – Spacing EnumGroup")]
    public sealed class HorizontalOrVerticalLayoutGroupSpacingEnumGroupMonoBinder
        : EnumGroupMonoBinder<HorizontalOrVerticalLayoutGroup, float>
    {
        /// <inheritdoc/>
        protected override void SetValue(HorizontalOrVerticalLayoutGroup element, float value)
        {
            if (this.RequireFinite(value))
                element.spacing = value;
        }
    }
}
