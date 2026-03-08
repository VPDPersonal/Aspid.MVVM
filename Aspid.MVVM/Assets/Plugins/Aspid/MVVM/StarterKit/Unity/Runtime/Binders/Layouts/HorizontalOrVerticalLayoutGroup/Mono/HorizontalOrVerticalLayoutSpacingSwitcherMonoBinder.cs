using UnityEngine;
using UnityEngine.UI;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<float, float>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterFloat;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that switches the <see cref="UnityEngine.UI.HorizontalOrVerticalLayoutGroup.spacing"/> property
    /// on a <see cref="UnityEngine.UI.HorizontalOrVerticalLayoutGroup"/> between two values based on a bound boolean ViewModel property.
    /// </summary>
    [AddBinderContextMenu(typeof(HorizontalOrVerticalLayoutGroup), serializePropertyNames: "m_Spacing", SubPath = "Switcher")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/LayoutGroup/HorizontalOrVertical/HorizontalOrVerticalLayoutGroup Binder – Spacing Switcher")]
    public sealed class HorizontalOrVerticalLayoutSpacingSwitcherMonoBinder : SwitcherMonoBinder<HorizontalOrVerticalLayoutGroup, float, Converter>
    {
        protected override void SetValue(float value) =>
            CachedComponent.spacing = value;
    }
}