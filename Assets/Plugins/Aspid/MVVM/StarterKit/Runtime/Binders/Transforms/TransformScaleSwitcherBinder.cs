#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public sealed class TransformScaleSwitcherBinder : SwitcherBinder<Vector3>
    {
        [Header("Component")]
        [SerializeField] private Transform _transform;
        
        [Header("Converter")]
        [SerializeField] private Vector3CombineConverter? _converter;

        public TransformScaleSwitcherBinder(Vector3 trueValue, Vector3 falseValue, Transform transform, Vector3CombineConverter? converter = null) 
            : base(trueValue, falseValue)
        {
            _converter = converter;
            _transform = transform ?? throw new ArgumentNullException(nameof(transform));
        }

        protected override void SetValue(Vector3 value) =>
            _transform.SetScale(value, _converter);
    }
}