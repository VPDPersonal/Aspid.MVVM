#nullable enable
using System;
using UnityEngine;
using System.Collections.Generic;
using Aspid.MVVM.StarterKit.Converters;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.Converters.IConverter<UnityEngine.Material?, UnityEngine.Material?>;
#else
using Converter = Aspid.MVVM.StarterKit.Converters.IConverterMaterial;
#endif

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class RendererMaterialsBinder : TargetBinder<Renderer>, IBinder<Material>, IBinder<Material[]>, IBinder<IReadOnlyCollection<Material>>
    {
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter? _converter;

        public RendererMaterialsBinder(Renderer target, Func<Material?, Material?> converter)
            : this(target, converter.ToConvert()) { }
        
        public RendererMaterialsBinder(Renderer target, Converter? converter = null)
            : base(target)
        {
            _converter = converter;
        }
        
        public void SetValue(Material? value) =>
            Target.material = _converter?.Convert(value) ?? value;
        
        public void SetValue(Material[]? values) =>
            Target.SetMaterials(_converter, values);
        
        public void SetValue(IReadOnlyCollection<Material>? values) =>
            Target.SetMaterials(_converter, values);
    }
}