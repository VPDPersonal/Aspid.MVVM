#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class SphereColliderRadiusSwitcherBinder : SwitcherBinder<float>
    {
        [Header("Component")]
        [SerializeField] private CapsuleCollider _collider;
        
#if UNITY_2023_1_OR_NEWER
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#endif
        private IConverter<float, float>? _converter;

        public SphereColliderRadiusSwitcherBinder(
            float trueValue, 
            float falseValue, 
            CapsuleCollider collider,
            Func<float, float> converter)
            : this(trueValue, falseValue, collider, converter.ToConvert()) { }
        
        public SphereColliderRadiusSwitcherBinder(
            float trueValue, 
            float falseValue, 
            CapsuleCollider collider,
            IConverter<float, float>? converter = null)
            : base(trueValue, falseValue)
        {
            _converter = converter;
            _collider = collider ?? throw new ArgumentNullException(nameof(collider));
        }

        protected override void SetValue(float value) =>
            _collider.radius = value;
    }
}