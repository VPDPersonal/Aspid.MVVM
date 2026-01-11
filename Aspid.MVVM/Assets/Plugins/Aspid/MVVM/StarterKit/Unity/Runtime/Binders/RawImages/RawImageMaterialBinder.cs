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
    public class RawImageMaterialBinder : TargetBinder<RawImage>, IBinder<Material?>
    {
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter? _converter;
        
        public RawImageMaterialBinder(RawImage target, BindMode mode = BindMode.OneWay)
            : this(target, converter: null, mode) { }
        
        public RawImageMaterialBinder(RawImage target, Converter? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            _converter = converter;
        }

        public void SetValue(Material? value) =>
            Target.material = _converter?.Convert(value) ?? value;
    }
}