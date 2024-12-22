using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Binders/UI/RectTransform/RectTransform Binder - SizeDelta EnumGroup")]
    public sealed class RectTransformSizeDeltaEnumGroupMonoBinder : EnumGroupMonoBinder<RectTransform>
    {
        [Header("Parameter")]
        [SerializeField] private Vector3 _defaultValue;
        [SerializeField] private Vector3 _selectedValue;
        [SerializeField] private SizeDeltaMode _mode = SizeDeltaMode.SizeDelta;
        
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#if UNITY_2023_1_OR_NEWER
        private IConverter<Vector2, Vector2> _defaultValueConverter;
#else
        private IConverterVector2 _defaultValueConverter;
#endif
        
        [SerializeReference]
        [SerializeReferenceDropdown]
#if UNITY_2023_1_OR_NEWER
        private IConverter<Vector2, Vector2> _selectedValueConverter;
#else
        private IConverterVector2 _selectedValueConverter;
#endif

        protected override void SetDefaultValue(RectTransform element)
        {
            var value = _defaultValueConverter?.Convert(_defaultValue) ?? _defaultValue;
            element.SetSizeDelta(value, _mode);
        }

        protected override void SetSelectedValue(RectTransform element)
        {
            var value = _selectedValueConverter?.Convert(_selectedValue) ?? _selectedValue;
            element.SetSizeDelta(value, _mode);
        }
    }
}