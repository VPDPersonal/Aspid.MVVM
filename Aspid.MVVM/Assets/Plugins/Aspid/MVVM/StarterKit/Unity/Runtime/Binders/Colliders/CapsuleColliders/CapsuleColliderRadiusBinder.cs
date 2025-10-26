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
    public class CapsuleColliderRadiusBinder : TargetBinder<CapsuleCollider>, INumberBinder
    {
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter? _converter;
        
        public CapsuleColliderRadiusBinder(CapsuleCollider target, BindMode mode)
            : this(target, null, mode) { }
        
        public CapsuleColliderRadiusBinder(
            CapsuleCollider target,
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