#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<float, float>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterFloat;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(TMP_Dropdown), SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Dropdown/Dropdown Binder – AlphaFadeSpeed EnumGroup")]
    public sealed class DropdownAlphaFadeSpeedEnumGroupMonoBinder : EnumGroupMonoBinder<TMP_Dropdown, float, Converter>
    {
        protected override void SetValue(TMP_Dropdown element, float value) =>
            element.alphaFadeSpeed = Mathf.Max(value, 0);
    }
}
#endif