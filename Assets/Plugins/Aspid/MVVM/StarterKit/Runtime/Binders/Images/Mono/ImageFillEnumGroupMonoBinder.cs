using UnityEngine;
using UnityEngine.UI;
using System.Runtime.CompilerServices;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("MVVM/Binders/UI/Image/Image Binder - Fill EnumGroup")]
    public sealed class ImageFillEnumGroupMonoBinder : EnumGroupMonoBinder<Image>
    {
        [Header("Parameters")]
        [SerializeField] [Range(0, 100)] private float _defaultValue;
        [SerializeField] [Range(0, 100)] private float _selectedValue;
        
        [Header("Converters")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#if UNITY_2023_1_OR_NEWER
        private IConverter<float, float> _defaultValueConverter;
#else
        private IConverterFloat _defaultValueConverter;
#endif
        
        [SerializeReference]
        [SerializeReferenceDropdown]
#if UNITY_2023_1_OR_NEWER
        private IConverter<float, float> _selectedValueConverter;
#else
        private IConverterFloat _selectedValueConverter;
#endif

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