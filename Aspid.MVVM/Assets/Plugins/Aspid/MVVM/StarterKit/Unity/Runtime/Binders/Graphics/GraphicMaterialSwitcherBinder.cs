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
    public sealed class GraphicMaterialSwitcherBinder : SwitcherBinder<Graphic, Material, Converter>
    {
        public GraphicMaterialSwitcherBinder(
            RawImage target, 
            Material trueValue, 
            Material falseValue,
            BindMode mode = BindMode.OneWay) 
            : this(target, trueValue, falseValue, converter: null, mode) { }
        
        public GraphicMaterialSwitcherBinder(
            RawImage target, 
            Material trueValue, 
            Material falseValue, 
            Converter? converter = null,
            BindMode mode = BindMode.OneWay) 
            : base(target, trueValue, falseValue, converter, mode) { }

        protected override void SetValue(Material? value) =>
            Target.material = value;
    }
}