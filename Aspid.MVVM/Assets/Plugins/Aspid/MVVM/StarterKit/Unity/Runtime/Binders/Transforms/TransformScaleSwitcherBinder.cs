#nullable enable
using System;
using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Vector3, UnityEngine.Vector3>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterVector3;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public sealed class TransformScaleSwitcherBinder : SwitcherBinder<Transform, Vector3, Converter>
    {
        public TransformScaleSwitcherBinder(
            Transform target, 
            Vector3 trueValue,
            Vector3 falseValue,
            BindMode mode = BindMode.OneWay) 
            : this(target, trueValue, falseValue, converter: null, mode) { }
        
        public TransformScaleSwitcherBinder(
            Transform target, 
            Vector3 trueValue,
            Vector3 falseValue, 
            Converter? converter,
            BindMode mode = BindMode.OneWay) 
            : base(target, trueValue, falseValue, converter, mode) { }

        protected override void SetValue(Vector3 value) =>
            Target.localScale = value;
    }
}