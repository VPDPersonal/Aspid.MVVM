using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.Unity;
using System.Runtime.CompilerServices;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<float, float>;
#else
using Converter = Aspid.MVVM.StarterKit.Unity.IConverterFloat;
#endif

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddPropertyContextMenu(typeof(Image), "m_FillAmount")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Image/Image Binder - Fill EnumGroup")]
    [AddComponentContextMenu(typeof(Image),"Add Image Binder/Image Binder - Fill EnumGroup")]
    public sealed class ImageFillEnumGroupMonoBinder : EnumGroupMonoBinder<Image>
    {
        [Header("Parameters")]
        [SerializeField] [Range(0, 1)] private float _defaultValue;
        [SerializeField] [Range(0, 1)] private float _selectedValue;
        
        [Header("Converters")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _defaultValueConverter;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _selectedValueConverter;

        protected override void SetDefaultValue(Image element) 
        {
            var value = _defaultValueConverter?.Convert(_defaultValue) ?? _defaultValue;
            SetValue(element, value);
        }

        protected override void SetSelectedValue(Image element)
        {
            var value = _selectedValueConverter?.Convert(_selectedValue) ?? _selectedValue;
            SetValue(element, value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void SetValue(Image element, float value) =>
            element.fillAmount = Mathf.Clamp01(value);
    }
}