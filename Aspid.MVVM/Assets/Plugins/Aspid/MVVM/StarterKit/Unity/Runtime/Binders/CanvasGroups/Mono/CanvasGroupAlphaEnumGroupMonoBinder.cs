using UnityEngine;
using System.Runtime.CompilerServices;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<float, float>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterFloat;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(CanvasGroup), serializePropertyNames: "m_Alpha")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Canvas Group/CanvasGroup Binder – Alpha EnumGroup")]
    public sealed class CanvasGroupAlphaEnumGroupMonoBinder : EnumGroupMonoBinder<CanvasGroup>
    {
        [Header("Values")]
        [SerializeField] [Range(0f, 1f)] private float _defaultValue;
        [SerializeField] [Range(0f, 1f)] private float _selectedValue;
        
        [Header("Converters")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _defaultValueConverter;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _selectedValueConverter;
        
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