#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class TransformRotationBinder : TargetBinder<Transform>, IRotationBinder, INumberBinder
    {
        [Header("Parameter")]
        [SerializeField] private Space _space;

#if UNITY_2023_1_OR_NEWER
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#endif
        private IConverter<Quaternion, Quaternion>? _converter;
        
        public TransformRotationBinder(Transform target, Space space = Space.World)
            : this(target, space, null as IConverter<Quaternion, Quaternion>) { }
        
        public TransformRotationBinder(Transform target, Func<Quaternion, Quaternion> converter) 
            : this(target, Space.World, converter.ToConvert()) { }
        
        public TransformRotationBinder(Transform target, Space space, Func<Quaternion, Quaternion> converter)
            : this(target, space, converter.ToConvert()) { }

        public TransformRotationBinder(Transform target, IConverter<Quaternion, Quaternion>? converter) 
            : this(target, Space.World, converter) { }
        
        public TransformRotationBinder(Transform target, Space space, IConverter<Quaternion, Quaternion>? converter)    
            : base(target)
        {
            _space = space;
            _converter = converter; 
        }
        
        public void SetValue(Quaternion value)
        {
            value = _converter?.Convert(value) ?? value;
            Target.SetRotation(value, _space);
        }
        
        public void SetValue(int value) =>
            SetValue((float)value);
        
        public void SetValue(long value) =>
            SetValue((float)value);
        
        public void SetValue(double value) =>
            SetValue((float)value);
        
        public void SetValue(float value) =>
            SetValue(Quaternion.Euler(new Vector3(value, value, value)));
    }
}