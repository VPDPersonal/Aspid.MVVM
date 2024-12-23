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
            Vector3CombineConverter? converter = null) 
            : base(target, trueValue, falseValue)
        {
            _converter = converter;
        }

        protected override void SetValue(Vector3 value) =>
            Target.SetScale(value, _converter);
    }
}