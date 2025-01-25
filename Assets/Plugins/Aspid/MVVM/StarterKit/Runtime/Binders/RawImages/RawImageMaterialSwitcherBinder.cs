#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.StarterKit.Converters;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.Converters.IConverter<UnityEngine.Material?, UnityEngine.Material?>;
#else
using Converter = Aspid.MVVM.StarterKit.Converters.IConverterMaterial;
#endif

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public sealed class RawImageMaterialSwitcherBinder : SwitcherBinder<RawImage, Material>
    {
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter? _converter;
        
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
            Converter? converter = null) 
            : base(target, trueValue, falseValue)
        {
            _converter = converter;
        }

        protected override void SetValue(Material value) =>
            Target.material = _converter?.Convert(value) ?? value;
    }
}