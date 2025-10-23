#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;
using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    public sealed class TextAlignmentEnumGroupMonoBinder : EnumGroupMonoBinder<TMP_Text>
    {
        [Header("Values")]
        [SerializeField] private TextAlignmentOptions _defaultValue;
        [SerializeField] private TextAlignmentOptions _selectedValue;

        protected override void SetDefaultValue(TMP_Text element) =>
            SetValue(element, _defaultValue);

        protected override void SetSelectedValue(TMP_Text element) =>
            SetValue(element, _selectedValue);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void SetValue(TMP_Text element, TextAlignmentOptions value) =>
            element.alignment = value;
    }
}
#endif