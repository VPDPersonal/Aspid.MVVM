#nullable enable
using System;
using UnityEngine;
using Aspid.UI.MVVM.StarterKit.Converters;

namespace Aspid.UI.MVVM.StarterKit.Binders.Transforms
{
    public class TransformScaleBinder : Binder, IVectorBinder, INumberBinder
    {
        private readonly Transform _transform;
        private readonly IConverter<Vector3, Vector3>? _converter;
        
        public TransformScaleBinder(Transform transform, Func<Vector3, Vector3> converter) :
            this(transform, new GenericFuncConverter<Vector3, Vector3>(converter)) { }
        
        public TransformScaleBinder(Transform transform, IConverter<Vector3, Vector3>? converter = null)
        {
            _converter = converter;
            _transform = transform ?? throw new ArgumentNullException(nameof(transform));
        }

        public void SetValue(Vector2 value) => SetValue((Vector3)value);

        public void SetValue(Vector3 value) =>
            _transform.localScale = _converter?.Convert(value) ?? value;

        public void SetValue(int value) => SetValue(Vector3.one * value);

        public void SetValue(long value) => SetValue(Vector3.one * value);
        
        public void SetValue(float value) => SetValue(Vector3.one * value);
        
        public void SetValue(double value) => SetValue(Vector3.one * (float)value);
    }
}