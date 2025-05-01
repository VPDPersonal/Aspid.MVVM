#nullable enable
using System;
using UnityEngine;

namespace Aspid.MVVM.StarterKit.Unity
{
    [Serializable]
    public sealed class TransformEulerAnglesSwitcherBinder : SwitcherBinder<Transform, Vector3>
    {
        [SerializeField] private Space _space;
        
        [Header("Converter")]
        [SerializeField] private Vector3CombineConverter? _converter;
        
        public TransformEulerAnglesSwitcherBinder(
            Transform target, 
            Vector3 trueValue, 
            Vector3 falseValue,
            BindMode mode)
            : this(target, trueValue, falseValue, Space.World, null, mode) { }
        
        public TransformEulerAnglesSwitcherBinder(
            Transform target, 
            Vector3 trueValue, 
            Vector3 falseValue, 
            Space space,
            BindMode mode)
            : this(target, trueValue, falseValue, space, null, mode) { }
        
        public TransformEulerAnglesSwitcherBinder(
            Transform target, 
            Vector3 trueValue, 
            Vector3 falseValue, 
            Vector3CombineConverter? converter,
            BindMode mode = BindMode.OneWay)
            : this(target, trueValue, falseValue, Space.World, converter, mode) { }
        
        public TransformEulerAnglesSwitcherBinder(
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
            Target.SetEulerAngles(value, _space, _converter);
    }
}