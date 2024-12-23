#nullable enable
using System;
using UnityEngine;
using System.Collections.Generic;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class RendererMaterialsBinder : TargetBinder<Renderer>, IBinder<Material>, IBinder<Material[]>, IBinder<IReadOnlyCollection<Material>>
    {
#if UNITY_2023_1_OR_NEWER
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#endif
        private IConverter<Material?, Material?>? _converter;

        public RendererMaterialsBinder(Renderer target, Func<Material?, Material?> converter)
            : this(target, converter.ToConvert()) { }
        
        public RendererMaterialsBinder(Renderer target, IConverter<Material?, Material?>? converter = null)
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