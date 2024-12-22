#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class TransformEulerAnglesBinder : Binder, IVectorBinder, INumberBinder
    {
        [Header("Component")]
        private Transform _transform;
        
        [Header("Parameter")]
        private Space _space;

        [Header("Converter")]
        [SerializeField] private Vector3CombineConverter? _converter;
        
        public TransformEulerAnglesBinder(Transform transform, Vector3CombineConverter? converter)
            : this(transform, Space.World, converter) { }
        
        public TransformEulerAnglesBinder(Transform transform, Space space = Space.World, Vector3CombineConverter? converter = null)
        {
            _space = space;
            _converter = converter;
            _transform = transform ?? throw new ArgumentNullException(nameof(transform));
        }
        
        public void SetValue(Vector2 value) =>
            SetValue((Vector3)value);
        
        public void SetValue(Vector3 value) =>
            _transform.SetEulerAngles(value, _space, _converter);
        
        public void SetValue(int value) =>
            SetValue((float)value);
        
        public void SetValue(long value) =>
            SetValue((float)value);
        
        public void SetValue(double value) =>
            SetValue((float)value);
        
        public void SetValue(float value) =>
            SetValue(new Vector3(value, value, value));
    }
}