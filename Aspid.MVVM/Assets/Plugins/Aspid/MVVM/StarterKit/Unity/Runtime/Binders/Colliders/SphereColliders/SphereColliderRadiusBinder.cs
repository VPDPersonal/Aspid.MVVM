#nullable enable
using System;
using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<float, float>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterFloat;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public class SphereColliderRadiusBinder : TargetBinder<SphereCollider>, INumberBinder
    {
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter? _converter;
        
        public SphereColliderRadiusBinder(SphereCollider target, BindMode mode)
            : this(target, converter: null,  mode) { }
        
        public SphereColliderRadiusBinder(
            SphereCollider target,
            Converter? converter = null, 
            BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
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