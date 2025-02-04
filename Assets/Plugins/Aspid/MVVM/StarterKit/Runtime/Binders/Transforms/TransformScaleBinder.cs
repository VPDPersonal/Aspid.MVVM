#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class TransformScaleBinder : TargetBinder<Transform>, IVectorBinder, INumberBinder
    {
        [Header("Converter")]
        [SerializeField] private Vector3CombineConverter? _converter;
        
        public TransformScaleBinder(Transform target,  BindMode mode = BindMode.OneWay)
            : this(target, null, mode) { }
        
        public TransformScaleBinder(Transform target, Vector3CombineConverter? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            _converter = converter;
        }

        public void SetValue(Vector2 value) => 
            SetValue((Vector3)value);

        public void SetValue(Vector3 value) =>
            Target.SetScale(value, _converter);

        public void SetValue(int value) => 
            SetValue(Vector3.one * value);

        public void SetValue(long value) => 
            SetValue(Vector3.one * value);
        
        public void SetValue(float value) => 
            SetValue(Vector3.one * value);
        
        public void SetValue(double value) => 
            SetValue(Vector3.one * (float)value);
    }
}