using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent, TValue}"/> that sets
    /// <see cref="HorizontalOrVerticalLayoutGroup.spacing"/>.
    /// </summary>
    /// <remarks>
    /// A non-finite value is refused.
    /// </remarks>
    [AddBinderContextMenu(typeof(HorizontalOrVerticalLayoutGroup), serializePropertyNames: "m_Spacing", SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/HorizontalOrVerticalLayoutGroup/HorizontalOrVerticalLayoutGroup Binder – Spacing Enum")]
    public sealed class HorizontalOrVerticalLayoutGroupSpacingEnumMonoBinder
        : EnumMonoBinder<HorizontalOrVerticalLayoutGroup, float>
    {
        /// <inheritdoc/>
        protected override void SetValue(float value)
        {
            if (this.RequireFinite(value))
                CachedComponent.spacing = value;
        }
    }
}
