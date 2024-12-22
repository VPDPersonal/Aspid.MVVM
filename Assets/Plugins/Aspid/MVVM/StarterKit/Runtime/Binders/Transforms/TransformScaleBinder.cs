#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class TransformScaleBinder : Binder, IVectorBinder, INumberBinder
    {
        [Header("Component")]
        private Transform _transform;
        
        [Header("Converter")]
        [SerializeField] private Vector3CombineConverter? _converter;
        
        public TransformScaleBinder(Transform transform, Vector3CombineConverter? converter = null)
        {
            _converter = converter;
            _transform = transform ?? throw new ArgumentNullException(nameof(transform));
        }

        public void SetValue(Vector2 value) => 
            SetValue((Vector3)value);

        public void SetValue(Vector3 value) =>
            _transform.SetScale(value, _converter);

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