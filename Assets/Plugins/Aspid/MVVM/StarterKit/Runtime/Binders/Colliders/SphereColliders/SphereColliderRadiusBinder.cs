#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class SphereColliderRadiusBinder : TargetBinder<SphereCollider>, INumberBinder
    {
#if UNITY_2023_1_OR_NEWER
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#endif
        private IConverter<float, float>? _converter;

        public SphereColliderRadiusBinder(SphereCollider target, Func<float, float> converter)
            : this(target, converter.ToConvert()) { }
        
        public SphereColliderRadiusBinder(SphereCollider target, IConverter<float, float>? converter = null)
            : base(target)
        {
            _converter = converter;
        }

        public void SetValue(int value) =>
            SetValue((float)value);

        public void SetValue(long value) =>
            SetValue((float)value);
        
        public void SetValue(float value) =>
            Target.radius = _converter?.Convert(value) ?? value;

        public void SetValue(double value) =>
            SetValue((float)value);
    }
}