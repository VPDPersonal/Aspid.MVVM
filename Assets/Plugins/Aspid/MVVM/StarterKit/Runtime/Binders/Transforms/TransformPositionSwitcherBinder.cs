#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public sealed class TransformPositionSwitcherBinder : SwitcherBinder<Transform, Vector3>
    {
        [SerializeField] private Space _space;

        [Header("Converter")]
        [SerializeField] private Vector3CombineConverter? _converter;
        
        public TransformPositionSwitcherBinder(
            Transform target,
            Vector3 trueValue,
            Vector3 falseValue, 
            Vector3CombineConverter? converter) 
            : this(target, trueValue, falseValue, Space.World, converter) { }
        
        public TransformPositionSwitcherBinder(
            Transform target,
            Vector3 trueValue,
            Vector3 falseValue, 
            Space space = Space.World,
            Vector3CombineConverter? converter = null) 
            : base(target, trueValue, falseValue)
        {
            _space = space;
            _converter = converter;
        }

        protected override void SetValue(Vector3 value) =>
            Target.SetPosition(value, _space, _converter);
    }
}