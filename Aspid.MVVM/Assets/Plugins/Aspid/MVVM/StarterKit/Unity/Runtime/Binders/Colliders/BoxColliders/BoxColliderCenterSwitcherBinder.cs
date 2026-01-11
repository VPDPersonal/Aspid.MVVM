#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public sealed class BoxColliderCenterSwitcherBinder : SwitcherBinder<BoxCollider, Vector3>
    {
        // ReSharper disable once MemberInitializerValueIgnored
        [SerializeField] private Vector3CombineConverter? _converter = Vector3CombineConverter.Default;
        
        public BoxColliderCenterSwitcherBinder(
            BoxCollider target, 
            Vector3 trueValue, 
            Vector3 falseValue,
            BindMode mode) 
            : this(target, trueValue, falseValue, converter: null, mode) { }
        
        public BoxColliderCenterSwitcherBinder(
            BoxCollider target,
            Vector3 trueValue, 
            Vector3 falseValue, 
            Vector3CombineConverter? converter = null,
            BindMode mode = BindMode.OneWay) 
            : base(target, trueValue, falseValue, mode)
        {
            _converter = converter;
        }

        protected override void SetValue(Vector3 value) =>
            Target.center = _converter?.Convert(value, Target.center) ?? value;
    }
}