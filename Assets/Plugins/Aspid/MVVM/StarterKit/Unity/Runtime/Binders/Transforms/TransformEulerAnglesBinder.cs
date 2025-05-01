#nullable enable
using System;
using UnityEngine;

namespace Aspid.MVVM.StarterKit.Unity
{
    [Serializable]
    public class TransformEulerAnglesBinder : TargetBinder<Transform>, IVectorBinder, INumberBinder
    {
        [Header("Parameter")]
        [SerializeField] private Space _space;

        [Header("Converter")]
        [SerializeField] private Vector3CombineConverter? _converter;

        public TransformEulerAnglesBinder(Transform target, BindMode mode)
            : this(target, Space.World, null, mode) { }
        
        public TransformEulerAnglesBinder(Transform target, Space space, BindMode mode)
            : this(target, space, null, mode) { }
        
        public TransformEulerAnglesBinder(
            Transform target,
            Vector3CombineConverter? converter,
            BindMode mode = BindMode.OneWay)
            : this(target, Space.World, converter, mode) { }
        
        public TransformEulerAnglesBinder(
            Transform target,
            Space space = Space.World,
            Vector3CombineConverter? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            
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