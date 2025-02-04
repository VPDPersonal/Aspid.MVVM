#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public sealed class TransformScaleSwitcherBinder : SwitcherBinder<Transform, Vector3>
    {
        [Header("Converter")]
        [SerializeField] private Vector3CombineConverter? _converter;

        public TransformScaleSwitcherBinder(
            Transform target, 
            Vector3 trueValue,
            Vector3 falseValue,
            BindMode mode) 
            : this(target, trueValue, falseValue, null, mode) { }
        
        public TransformScaleSwitcherBinder(
            Transform target, 
            Vector3 trueValue,
            Vector3 falseValue, 
            Vector3CombineConverter? converter = null,
            BindMode mode = BindMode.OneWay) 
            : base(target, trueValue, falseValue, mode)
        {
            _converter = converter;
        }

        protected override void SetValue(Vector3 value) =>
            Target.SetScale(value, _converter);
    }
}