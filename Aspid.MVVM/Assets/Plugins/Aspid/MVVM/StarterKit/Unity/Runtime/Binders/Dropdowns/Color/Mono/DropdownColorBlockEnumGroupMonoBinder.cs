#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.UI.ColorBlock, UnityEngine.UI.ColorBlock>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterColorBlock;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(TMP_Dropdown), SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Dropdown/Dropdown Binder – ColorBlock EnumGroup")]
    public sealed class DropdownColorBlockEnumGroupMonoBinder : EnumGroupMonoBinder<TMP_Dropdown, ColorBlock, Converter>
    {
        protected override void SetValue(TMP_Dropdown element, ColorBlock value) =>
            element.colors = value;
    }
}
#endif