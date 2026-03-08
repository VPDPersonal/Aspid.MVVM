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
    /// MonoBehaviour binder that sets the <see cref="UnityEngine.UI.HorizontalOrVerticalLayoutGroup.spacing"/> property
    /// on a group of <see cref="UnityEngine.UI.HorizontalOrVerticalLayoutGroup"/> components, applying the configured
    /// selected or default value to each entry based on the bound enum ViewModel value.
    /// </summary>
    [AddBinderContextMenu(typeof(HorizontalOrVerticalLayoutGroup), serializePropertyNames: "m_Spacing", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/LayoutGroup/HorizontalOrVertical/HorizontalOrVerticalLayoutGroup Binder – Spacing EnumGroup")]
    public sealed class HorizontalOrVerticalLayoutGroupSpacingEnumGroupMonoBinder : EnumGroupMonoBinder<HorizontalOrVerticalLayoutGroup, float, Converter>
    {
        protected override void SetValue(HorizontalOrVerticalLayoutGroup element, float value) =>
            element.spacing = value;
    }
}