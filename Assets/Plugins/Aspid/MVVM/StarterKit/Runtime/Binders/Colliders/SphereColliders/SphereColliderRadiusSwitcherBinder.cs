#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public sealed class SphereColliderRadiusSwitcherBinder : SwitcherBinder<CapsuleCollider, float>
    {
#if UNITY_2023_1_OR_NEWER
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#endif
        private IConverter<float, float>? _converter;

        public SphereColliderRadiusSwitcherBinder(
            CapsuleCollider target,
            float trueValue, 
            float falseValue, 
            Func<float, float> converter)
            : this(target, trueValue, falseValue, converter.ToConvert()) { }
        
        public SphereColliderRadiusSwitcherBinder(
            CapsuleCollider target,
            float trueValue, 
            float falseValue, 
            IConverter<float, float>? converter = null)
            : base(target, trueValue, falseValue)
        {
            _converter = converter;
        }

        protected override void SetValue(float value) =>
            Target.radius = _converter?.Convert(value) ?? value;
    }
}