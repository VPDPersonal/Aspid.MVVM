using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherFloatMonoBinder{HorizontalOrVerticalLayoutGroup}"/> that switches the
    /// <see cref="UnityEngine.UI.HorizontalOrVerticalLayoutGroup.spacing"/> property between two values
    /// based on the bound boolean ViewModel value.
    /// </summary>
    [AddBinderContextMenu(typeof(HorizontalOrVerticalLayoutGroup), serializePropertyNames: "m_Spacing", SubPath = "Switcher")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/LayoutGroup/HorizontalOrVertical/HorizontalOrVerticalLayoutGroup Binder – Spacing Switcher")]
    public sealed class HorizontalOrVerticalLayoutSpacingSwitcherMonoBinder : SwitcherFloatMonoBinder<HorizontalOrVerticalLayoutGroup>
    {
        /// <summary>
        /// Sets <see cref="UnityEngine.UI.HorizontalOrVerticalLayoutGroup.spacing"/> to <paramref name="value"/> if it is finite.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        protected override void SetValue(float value)
        {
            if (!BinderMath.IsFinite(value)) return;
            CachedComponent.spacing = value;
        }
    }
}