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
    public class RawImageMaterialBinder : TargetBinder<RawImage>, IBinder<Material?>
    {
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter? _converter;

        public RawImageMaterialBinder(RawImage target, Func<Material?, Material?> converter)
            : this(target, converter.ToConvert()) { }
        
        public RawImageMaterialBinder(RawImage target, Converter? converter = null)
            : base(target)
        {
            _converter = converter;
        }

        public void SetValue(Material? value) =>
            Target.material = _converter?.Convert(value) ?? value;
    }
}