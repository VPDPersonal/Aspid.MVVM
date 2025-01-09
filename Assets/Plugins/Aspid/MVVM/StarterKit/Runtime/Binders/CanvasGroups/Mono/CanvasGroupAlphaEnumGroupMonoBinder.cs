using UnityEngine;
using System.Runtime.CompilerServices;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("MVVM/Binders/UI/Canvas Group/CanvasGroup Binder - Alpha EnumGroup")]
    public sealed class CanvasGroupAlphaEnumGroupMonoBinder : EnumGroupMonoBinder<CanvasGroup>
    {
        [Header("Parameters")]
        [SerializeField] [Range(0f, 1f)] private float _defaultValue;
        [SerializeField] [Range(0f, 1f)] private float _selectedValue;
        
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
        
        protected override void SetDefaultValue(CanvasGroup element) 
        {
            var value = _defaultValueConverter?.Convert(_defaultValue) ?? _defaultValue;
            SetValue(element, value);
        }

        protected override void SetSelectedValue(CanvasGroup element)
        {
            var value = _selectedValueConverter?.Convert(_selectedValue) ?? _selectedValue;
            SetValue(element, value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void SetValue(CanvasGroup element, float value) =>
            element.alpha = Mathf.Clamp01(value);
    }
}