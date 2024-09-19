using System;
using UnityEngine;
using AspidUI.MVVM.StarterKit.Converters;

namespace AspidUI.MVVM.StarterKit.Binders.Transforms
{
    public class TransformScaleBinder : Binder, IVectorBinder, INumberBinder
    {
        protected readonly Transform Transform;
        protected readonly IConverter<Vector3, Vector3> Converter;
        
        public TransformScaleBinder(Transform transform, Func<Vector3, Vector3> converter) :
            this(transform, new GenericFuncConverter<Vector3, Vector3>(converter)) { }
        
        public TransformScaleBinder(Transform transform, IConverter<Vector3, Vector3> converter = null)
        {
            Transform = transform;
            Converter = converter;
        }

        public void SetValue(Vector2 value) =>
            SetValue((Vector3)value);

        public void SetValue(Vector3 value) =>
            Transform.localScale = Converter?.Convert(value) ?? value;

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