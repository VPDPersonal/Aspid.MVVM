#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class TransformEulerAnglesBinder : TargetBinder<Transform>, IVectorBinder, INumberBinder
    {
        [Header("Parameter")]
        private Space _space;

        [Header("Converter")]
        [SerializeField] private Vector3CombineConverter? _converter;
        
        public TransformEulerAnglesBinder(Transform target, Vector3CombineConverter? converter)
            : this(target, Space.World, converter) { }
        
        public TransformEulerAnglesBinder(Transform target, Space space = Space.World, Vector3CombineConverter? converter = null)
            : base(target)
        {
            _space = space;
            _converter = converter;
        }
        
        public void SetValue(Vector2 value) =>
            SetValue((Vector3)value);
        
        public void SetValue(Vector3 value) =>
            Target.SetEulerAngles(value, _space, _converter);
        
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