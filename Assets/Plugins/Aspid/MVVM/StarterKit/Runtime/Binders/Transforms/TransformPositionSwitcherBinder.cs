#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public sealed class TransformPositionSwitcherBinder : SwitcherBinder<Vector3>
    {
        [SerializeField] private Space _space;
        
        [Header("Component")]
        [SerializeField] private Transform _transform;
        
        [Header("Converter")]
        [SerializeField] private Vector3CombineConverter? _converter;
        
        public TransformPositionSwitcherBinder(
            Vector3 trueValue,
            Vector3 falseValue, 
            Transform transform,
            Vector3CombineConverter? converter) 
            : this(trueValue, falseValue, transform, Space.World, converter) { }
        
        public TransformPositionSwitcherBinder(
            Vector3 trueValue,
            Vector3 falseValue, 
            Transform transform,
            Space space = Space.World,
            Vector3CombineConverter? converter = null) 
            : base(trueValue, falseValue)
        {
            _space = space;
            _converter = converter;
            _transform = transform ?? throw new ArgumentNullException(nameof(transform));
        }

        protected override void SetValue(Vector3 value) =>
            _transform.SetPosition(value, _space, _converter);
    }
}