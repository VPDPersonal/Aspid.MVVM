#nullable enable
using System;
using UnityEngine;
using System.Collections.Generic;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class RendererMaterialsBinder : Binder, IBinder<Material>, IBinder<Material[]>, IBinder<IReadOnlyCollection<Material>>
    {
        [Header("Component")]
        [SerializeField] private Renderer _renderer;
        
#if UNITY_2023_1_OR_NEWER
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#endif
        private IConverter<Material?, Material?>? _converter;

        public RendererMaterialsBinder(Renderer renderer, Func<Material?, Material?> converter)
            : this(renderer, converter.ToConvert()) { }
        
        public RendererMaterialsBinder(Renderer renderer, IConverter<Material?, Material?>? converter = null)
        {
            _converter = converter;
            _renderer = renderer ?? throw new ArgumentNullException(nameof(renderer));
        }
        
        public void SetValue(Material? value) =>
            _renderer.material = _converter?.Convert(value) ?? value;
        
        public void SetValue(Material[]? values) =>
            _renderer.SetMaterials(_converter, values);
        
        public void SetValue(IReadOnlyCollection<Material>? values) =>
            _renderer.SetMaterials(_converter, values);
    }
}