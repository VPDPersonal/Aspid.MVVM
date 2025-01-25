#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public sealed class CapsuleColliderCenterSwitcherBinder : SwitcherBinder<CapsuleCollider, Vector3>
    {
        // ReSharper disable once MemberInitializerValueIgnored
        [Header("Converter")]
        [SerializeField] private Vector3CombineConverter? _converter = Vector3CombineConverter.Default;
        
        public CapsuleColliderCenterSwitcherBinder(
            CapsuleCollider target,
            Vector3 trueValue, 
            Vector3 falseValue, 
            Vector3CombineConverter? converter = null) 
            : base(target, trueValue, falseValue)
        {
            _converter = converter;
        }

        protected override void SetValue(Vector3 value) =>
            Target.center = _converter?.Convert(value, Target.center) ?? value;
    }
}