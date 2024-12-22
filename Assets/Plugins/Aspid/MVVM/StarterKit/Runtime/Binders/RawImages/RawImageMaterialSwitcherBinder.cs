#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public sealed class RawImageMaterialSwitcherBinder : SwitcherBinder<Material>
    {
        [Header("Component")]
        [SerializeField] private RawImage _image;

#if UNITY_2023_1_OR_NEWER
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#endif
        private IConverter<Material?, Material?>? _converter;
        
        public RawImageMaterialSwitcherBinder(
            Material trueValue, 
            Material falseValue, 
            RawImage image, 
            Func<Material?, Material?> converter) 
            : this(trueValue, falseValue, image, converter.ToConvert()) { }
        
        public RawImageMaterialSwitcherBinder(
            Material trueValue, 
            Material falseValue, 
            RawImage image, 
            IConverter<Material?, Material?>? converter = null) 
            : base(trueValue, falseValue)
        {
            _converter = converter;
            _image = image ?? throw new ArgumentNullException(nameof(image));
        }

        protected override void SetValue(Material value) =>
            _image.material = _converter?.Convert(value) ?? value;
    }
}