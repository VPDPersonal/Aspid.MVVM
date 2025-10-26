#nullable enable
using System;
using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Quaternion, UnityEngine.Quaternion>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterQuaternion;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public class TransformRotationBinder : TargetBinder<Transform>, IRotationBinder, INumberBinder
    {
        [SerializeField] private Space _space;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter? _converter;

        public TransformRotationBinder(Transform target, BindMode mode) 
            : this(target, Space.World, null, mode) { }
        
        public TransformRotationBinder(Transform target, Space space, BindMode mode) 
            : this(target, space, null, mode) { }
        
        public TransformRotationBinder(Transform target, Converter? converter, BindMode mode = BindMode.OneWay) 
            : this(target, Space.World, converter, mode) { }
        
        public TransformRotationBinder(
            Transform target,
            Space space = Space.World, 
            Converter? converter = null,
            BindMode mode = BindMode.OneWay)    
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            
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