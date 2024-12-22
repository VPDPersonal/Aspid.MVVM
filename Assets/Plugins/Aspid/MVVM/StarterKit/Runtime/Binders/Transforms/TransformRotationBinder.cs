#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class TransformRotationBinder : Binder, IRotationBinder, INumberBinder
    {
        [Header("Component")]
        [SerializeField] private Transform _transform;
        
        [Header("Parameter")]
        [SerializeField] private Space _space;

#if UNITY_2023_1_OR_NEWER
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#endif
        private IConverter<Quaternion, Quaternion>? _converter;
        
        public TransformRotationBinder(Transform transform, Space space = Space.World)
            : this(transform, space, null as IConverter<Quaternion, Quaternion>) { }
        
        public TransformRotationBinder(Transform transform, Func<Quaternion, Quaternion> converter) 
            : this(transform, Space.World, new GenericFuncConverter<Quaternion, Quaternion>(converter)) { }
        
        public TransformRotationBinder(Transform transform, Space space, Func<Quaternion, Quaternion> converter)
            : this(transform, space, new GenericFuncConverter<Quaternion, Quaternion>(converter)) { }

        public TransformRotationBinder(Transform transform, IConverter<Quaternion, Quaternion>? converter) 
            : this(transform, Space.World, converter) { }
        
        public TransformRotationBinder(Transform transform, Space space, IConverter<Quaternion, Quaternion>? converter)
        {
            _space = space;
            _converter = converter;
            _transform = transform ?? throw new ArgumentNullException(nameof(transform));
        }
        
        public void SetValue(Quaternion value)
        {
            value = _converter?.Convert(value) ?? value;
            _transform.SetRotation(value, _space);
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