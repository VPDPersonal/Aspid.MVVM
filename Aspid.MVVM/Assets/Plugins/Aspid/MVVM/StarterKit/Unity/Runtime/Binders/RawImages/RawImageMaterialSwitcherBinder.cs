#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Material?, UnityEngine.Material?>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterMaterial;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public sealed class RawImageMaterialSwitcherBinder : SwitcherBinder<RawImage, Material>
    {
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter? _converter;
        
        public RawImageMaterialSwitcherBinder(
            RawImage target, 
            Material trueValue, 
            Material falseValue,
            BindMode mode = BindMode.OneWay) 
            : this(target, trueValue, falseValue, converter: null, mode) { }
        
        public RawImageMaterialSwitcherBinder(
            RawImage target, 
            Material trueValue, 
            Material falseValue, 
            Converter? converter = null,
            BindMode mode = BindMode.OneWay) 
            : base(target, trueValue, falseValue, mode)
        {
            _converter = converter;
        }

        protected override void SetValue(Material value) =>
            Target.material = _converter?.Convert(value) ?? value;
    }
}