#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public sealed class CapsuleColliderCentreSwitcherBinder : SwitcherBinder<Vector3>
    {
        [Header("Component")]
        [SerializeField] private CapsuleCollider _collider;
        
        // ReSharper disable once MemberInitializerValueIgnored
        [Header("Converter")]
        [SerializeField] private Vector3CombineConverter? _converter = Vector3CombineConverter.Default;
        
        public CapsuleColliderCentreSwitcherBinder(
            Vector3 trueValue, 
            Vector3 falseValue, 
            CapsuleCollider collider,
            Vector3CombineConverter? converter = null) 
            : base(trueValue, falseValue)
        {
            _converter = converter;
            _collider = collider ?? throw new ArgumentNullException(nameof(collider));
        }

        protected override void SetValue(Vector3 value) =>
            _collider.center = _converter?.Convert(value, _collider.center) ?? value;
    }
}