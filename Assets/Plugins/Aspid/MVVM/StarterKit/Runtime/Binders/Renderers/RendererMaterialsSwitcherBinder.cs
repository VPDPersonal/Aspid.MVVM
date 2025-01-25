#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.Converters.IConverter<UnityEngine.Material?, UnityEngine.Material?>;
#else
using Converter = Aspid.MVVM.StarterKit.Converters.IConverterMaterial;
#endif

namespace Aspid.MVVM.StarterKit.Binders
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
            Func<Material?, Material?> converter)
            : this(target, trueValue, falseValue, converter.ToConvert()) { }
        
        public RendererMaterialsSwitcherBinder(
            Renderer target, 
            Material[]? trueValue,
            Material[]? falseValue,
            Converter? converter = null)
            : base(target, trueValue, falseValue)
        {
            _converter = converter;
        }

        protected override void SetValue(Material[]? values) =>
            Target.SetMaterials(_converter, values);
    }
}