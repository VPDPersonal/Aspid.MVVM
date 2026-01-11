#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public sealed class TransformPositionSwitcherBinder : SwitcherBinder<Transform, Vector3>
    {
        [SerializeField] private Space _space;
        [SerializeField] private Vector3CombineConverter? _converter;
       
        public TransformPositionSwitcherBinder(
            Transform target,
            Vector3 trueValue,
            Vector3 falseValue, 
            BindMode mode) 
            : this(target, trueValue, falseValue, Space.World, converter: null, mode) { }
        
        public TransformPositionSwitcherBinder(
            Transform target,
            Vector3 trueValue,
            Vector3 falseValue, 
            Space space,
            BindMode mode) 
            : this(target, trueValue, falseValue, space, converter: null, mode) { }
        
        public TransformPositionSwitcherBinder(
            Transform target,
            Vector3 trueValue,
            Vector3 falseValue, 
            Vector3CombineConverter? converter,
            BindMode mode = BindMode.OneWay) 
            : this(target, trueValue, falseValue, Space.World, converter, mode) { }
        
        public TransformPositionSwitcherBinder(
            Transform target,
            Vector3 trueValue,
            Vector3 falseValue, 
            Space space = Space.World,
            Vector3CombineConverter? converter = null,
            BindMode mode = BindMode.OneWay) 
            : base(target, trueValue, falseValue, mode)
        {
            _space = space;
            _converter = converter;
        }

        protected override void SetValue(Vector3 value) =>
            Target.SetPosition(value, _space, _converter);
    }
}