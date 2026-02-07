using UnityEngine;
using UnityEngine.UI;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.RectOffset, UnityEngine.RectOffset>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterRectOffset;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Layout/LayoutGroup Binder – Padding EnumGroup")]
    [AddBinderContextMenu(typeof(LayoutGroup), serializePropertyNames: "m_Padding", SubPath = "EnumGroup")]
    public sealed class LayoutGroupPaddingEnumGroupMonoBinder : EnumGroupMonoBinder<LayoutGroup>
    {
        [SerializeField] private RectOffset _defaultValue;
        [SerializeField] private RectOffset _selectedValue;
        
        [SerializeField] private PaddingMode _paddingMode;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _defaultValueConverter;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _selectedValueConverter;

        protected override void SetDefaultValue(LayoutGroup element) =>
            SetValue(element, _defaultValueConverter?.Convert(_defaultValue) ?? _defaultValue);

        protected override void SetSelectedValue(LayoutGroup element) =>
            SetValue(element, _selectedValueConverter?.Convert(_selectedValue) ?? _selectedValue);
        
        private void SetValue(LayoutGroup element, RectOffset value) =>
            element.SetPadding(value.top, value.right, value.bottom, value.left, _paddingMode);
    }
}