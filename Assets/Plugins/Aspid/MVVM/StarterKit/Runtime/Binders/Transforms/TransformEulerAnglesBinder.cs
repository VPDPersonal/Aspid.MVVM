#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    public class TransformEulerAnglesBinder : Binder, IVectorBinder, INumberBinder
    {
        private readonly Space _space;
        private readonly VectorMode _mode;
        private readonly Transform _transform;
        private readonly IConverter<Vector3, Vector3>? _converter;
        
        public TransformEulerAnglesBinder(Transform transform, Space space = Space.World, VectorMode mode = VectorMode.XYZ)
        {
            _space = space;
            _converter = null;
            _transform = transform;
        }
        
        public TransformEulerAnglesBinder(Transform transform, Func<Vector3, Vector3> converter) 
            : this(transform, Space.World, VectorMode.XYZ, new GenericFuncConverter<Vector3, Vector3>(converter)) { }
        
        public TransformEulerAnglesBinder(Transform transform, Space space, VectorMode mode, Func<Vector3, Vector3> converter)
            : this(transform, space, mode, new GenericFuncConverter<Vector3, Vector3>(converter)) { }

        public TransformEulerAnglesBinder(Transform transform, IConverter<Vector3, Vector3>? converter) :
            this(transform, Space.World, VectorMode.XYZ, converter) { }
        
        public TransformEulerAnglesBinder(Transform transform, Space space, VectorMode mode, IConverter<Vector3, Vector3>? converter)
        {
            _mode = mode;
            _space = space;
            _converter = converter;
            _transform = transform ?? throw new ArgumentNullException(nameof(transform));
        }
        
        public void SetValue(Vector2 value) => SetValue((Vector3)value);
        
        public void SetValue(Vector3 value)
        {
            value = _converter?.Convert(value) ?? value;
            _transform.SetEulerAngles(value, _mode, _space);
        }
        
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