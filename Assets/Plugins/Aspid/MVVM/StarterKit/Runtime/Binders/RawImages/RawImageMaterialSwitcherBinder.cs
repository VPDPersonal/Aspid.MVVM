#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public sealed class RawImageMaterialSwitcherBinder : SwitcherBinder<RawImage, Material>
    {
#if UNITY_2023_1_OR_NEWER
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#endif
        private IConverter<Material?, Material?>? _converter;
        
        public RawImageMaterialSwitcherBinder(
            RawImage target, 
            Material trueValue, 
            Material falseValue, 
            Func<Material?, Material?> converter) 
            : this(target, trueValue, falseValue, converter.ToConvert()) { }
        
        public RawImageMaterialSwitcherBinder(
            RawImage target, 
            Material trueValue, 
            Material falseValue, 
            IConverter<Material?, Material?>? converter = null) 
            : base(target, trueValue, falseValue)
        {
            _converter = converter;
        }

        protected override void SetValue(Material value) =>
            Target.material = _converter?.Convert(value) ?? value;
    }
}