#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class RawImageMaterialBinder : TargetBinder<RawImage>, IBinder<Material?>
    {
#if UNITY_2023_1_OR_NEWER
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#endif
        private IConverter<Material?, Material?>? _converter;

        public RawImageMaterialBinder(RawImage target, Func<Material?, Material?> converter)
            : this(target, converter.ToConvert()) { }
        
        public RawImageMaterialBinder(RawImage target, IConverter<Material?, Material?>? converter = null)
            : base(target)
        {
            _converter = converter;
        }

        public void SetValue(Material? value) =>
            Target.material = _converter?.Convert(value) ?? value;
    }
}