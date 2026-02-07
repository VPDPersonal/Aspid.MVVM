using UnityEngine;
using UnityEngine.UI;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Texture2D, UnityEngine.Texture2D>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterTexture2D;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Raw Image/RawImage Binder – Texture EnumGroup")]
    [AddBinderContextMenu(typeof(RawImage), serializePropertyNames: "m_Texture", SubPath = "EnumGroup")]
    public sealed class RawImageTextureEnumGroupMonoBinder : EnumGroupMonoBinder<RawImage>
    {
        [SerializeField] private Texture2D _defaultValue;
        [SerializeField] private Texture2D _selectedValue;
        
        [SerializeField] private bool _disabledWhenNull = true;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _defaultValueConverter;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _selectedValueConverter;

        protected override void SetDefaultValue(RawImage element) =>
            SetValue(element, _defaultValueConverter?.Convert(_defaultValue) ?? _defaultValue);

        protected override void SetSelectedValue(RawImage element) =>
            SetValue(element, _selectedValueConverter?.Convert(_selectedValue) ?? _selectedValue);
        
        private void SetValue(RawImage element, Texture2D value) 
        {
            element.texture = value;
            element.enabled = !_disabledWhenNull || value;
        }
    }
}