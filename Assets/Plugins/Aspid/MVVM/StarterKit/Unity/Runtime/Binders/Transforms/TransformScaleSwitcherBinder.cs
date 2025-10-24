#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public sealed class TransformScaleSwitcherBinder : SwitcherBinder<Transform, Vector3>
    {
        [Header("Converters")]
        [SerializeField] private Vector3CombineConverter? _converter;

        public TransformScaleSwitcherBinder(
            Transform target, 
            Vector3 trueValue,
            Vector3 falseValue,
            BindMode mode = BindMode.OneWay) 
            : this(target, trueValue, falseValue, null, mode) { }
        
        public TransformScaleSwitcherBinder(
            Transform target, 
            Vector3 trueValue,
            Vector3 falseValue, 
            Vector3CombineConverter? converter,
            BindMode mode = BindMode.OneWay) 
            : base(target, trueValue, falseValue, mode)
        {
            _converter = converter;
        }

        protected override void SetValue(Vector3 value) =>
            Target.SetScale(value, _converter);
    }
}