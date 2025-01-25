#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.Converters.IConverter<UnityEngine.Quaternion, UnityEngine.Quaternion>;
#else
using Converter = Aspid.MVVM.StarterKit.Converters.IConverterQuaternion;
#endif

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class TransformRotationBinder : TargetBinder<Transform>, IRotationBinder, INumberBinder
    {
        [Header("Parameter")]
        [SerializeField] private Space _space;
        
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter? _converter;
        
        public TransformRotationBinder(Transform target, Space space = Space.World)
            : this(target, space, null as Converter) { }
        
        public TransformRotationBinder(Transform target, Func<Quaternion, Quaternion> converter) 
            : this(target, Space.World, converter.ToConvert()) { }
        
        public TransformRotationBinder(Transform target, Space space, Func<Quaternion, Quaternion> converter)
            : this(target, space, converter.ToConvert()) { }

        public TransformRotationBinder(Transform target, Converter? converter) 
            : this(target, Space.World, converter) { }
        
        public TransformRotationBinder(Transform target, Space space, Converter? converter)    
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