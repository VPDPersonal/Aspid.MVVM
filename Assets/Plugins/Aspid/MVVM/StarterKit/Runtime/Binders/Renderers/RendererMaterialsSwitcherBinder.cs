#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public sealed class RendererMaterialsSwitcherBinder : SwitcherBinder<Material[]?>
    {
        [Header("Component")]
        [SerializeField] private Renderer _renderer;
        
#if UNITY_2023_1_OR_NEWER
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#endif
        private IConverter<Material?, Material?>? _converter;

        public RendererMaterialsSwitcherBinder(
            Material[]? trueValue,
            Material[]? falseValue,
            Renderer renderer,
            Func<Material?, Material?> converter)
            : this(trueValue, falseValue, renderer, converter.ToConvert()) { }
        
        public RendererMaterialsSwitcherBinder(
            Material[]? trueValue,
            Material[]? falseValue,
            Renderer? renderer, 
            IConverter<Material?, Material?>? converter = null)
            : base(trueValue, falseValue)
        {
            _converter = converter;
            _renderer = renderer ?? throw new ArgumentNullException(nameof(renderer));
        }

        protected override void SetValue(Material[]? values) =>
            _renderer.SetMaterials(_converter, values);
    }
}