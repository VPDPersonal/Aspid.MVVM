#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    public class TransformScaleBinder : Binder, IVectorBinder, INumberBinder
    {
        private readonly VectorMode _mode;
        private readonly Transform _transform;
        private readonly IConverter<Vector3, Vector3>? _converter;
        
        public TransformScaleBinder(Transform transform, Func<Vector3, Vector3> converter) :
            this(transform, VectorMode.XYZ, new GenericFuncConverter<Vector3, Vector3>(converter)) { }
        
        public TransformScaleBinder(Transform transform, VectorMode mode, Func<Vector3, Vector3> converter) :
            this(transform, mode, new GenericFuncConverter<Vector3, Vector3>(converter)) { }
        
        public TransformScaleBinder(Transform transform, VectorMode mode = VectorMode.XYZ, IConverter<Vector3, Vector3>? converter = null)
        {
            _mode = mode;
            _converter = converter;
            _transform = transform ?? throw new ArgumentNullException(nameof(transform));
        }

        public void SetValue(Vector2 value) => SetValue((Vector3)value);

        public void SetValue(Vector3 value) =>
            _transform.SetScale(value, _mode);

        public void SetValue(int value) => SetValue(Vector3.one * value);

        public void SetValue(long value) => SetValue(Vector3.one * value);
        
        public void SetValue(float value) => SetValue(Vector3.one * value);
        
        public void SetValue(double value) => SetValue(Vector3.one * (float)value);
    }
}