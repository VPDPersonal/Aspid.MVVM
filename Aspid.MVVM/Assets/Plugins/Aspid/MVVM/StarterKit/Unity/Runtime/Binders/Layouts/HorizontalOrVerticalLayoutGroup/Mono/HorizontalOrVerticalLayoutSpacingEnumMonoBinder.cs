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
    /// on a <see cref="UnityEngine.UI.HorizontalOrVerticalLayoutGroup"/> to a value resolved from an enum bound on the ViewModel.
    /// </summary>
    [AddBinderContextMenu(typeof(HorizontalOrVerticalLayoutGroup), serializePropertyNames: "m_Spacing", SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/LayoutGroup/HorizontalOrVertical/HorizontalOrVerticalLayoutGroup Binder – Spacing Enum")]
    public sealed class HorizontalOrVerticalLayoutSpacingEnumMonoBinder : EnumMonoBinder<HorizontalOrVerticalLayoutGroup, float, Converter>
    {
        protected override void SetValue(float value) =>
            CachedComponent.spacing = value;
    }
}