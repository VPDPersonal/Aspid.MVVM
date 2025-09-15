#nullable enable
using System;
using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Material?, UnityEngine.Material?>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterMaterial;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public sealed class RendererMaterialsSwitcherBinder : SwitcherBinder<Renderer, Material[]?>
    {
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter? _converter;
        
        public RendererMaterialsSwitcherBinder(
            Renderer target, 
            Material[]? trueValue,
            Material[]? falseValue,
            BindMode mode)
            : this(target, trueValue, falseValue, null, mode) { }
        
        public RendererMaterialsSwitcherBinder(
            Renderer target, 
            Material[]? trueValue,
            Material[]? falseValue,
            Converter? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, mode)
        {
            _converter = converter;
        }

        protected override void SetValue(Material[]? values) =>
            Target.SetMaterials(_converter, values);
    }
}