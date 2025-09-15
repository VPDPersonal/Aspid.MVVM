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
    public sealed class CapsuleColliderRadiusSwitcherBinder : SwitcherBinder<CapsuleCollider, float>
    {
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter? _converter;

        public CapsuleColliderRadiusSwitcherBinder(
            CapsuleCollider target, 
            float trueValue, 
            float falseValue,
            BindMode mode)
            : this(target, trueValue, falseValue, null, mode) { }
        
        public CapsuleColliderRadiusSwitcherBinder(
            CapsuleCollider target, 
            float trueValue, 
            float falseValue, 
            Converter? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, mode)
        {
            _converter = converter;
        }

        protected override void SetValue(float value) =>
            Target.radius = _converter?.Convert(value) ?? value;
    }
}