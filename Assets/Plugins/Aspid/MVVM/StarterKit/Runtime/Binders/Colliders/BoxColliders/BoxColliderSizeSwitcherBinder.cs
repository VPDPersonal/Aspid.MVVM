#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public sealed class BoxColliderSizeSwitcherBinder : SwitcherBinder<BoxCollider, Vector3>
    {
        // ReSharper disable once MemberInitializerValueIgnored
        [Header("Converter")]
        [SerializeField] private Vector3CombineConverter? _converter = Vector3CombineConverter.Default;
        
        public BoxColliderSizeSwitcherBinder(
            BoxCollider target,
            Vector3 trueValue, 
            Vector3 falseValue,
            BindMode mode) 
            : this(target, trueValue, falseValue, null, mode) { }
        
        public BoxColliderSizeSwitcherBinder(
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
            Target.size = _converter?.Convert(value, Target.size) ?? value;
    }
}