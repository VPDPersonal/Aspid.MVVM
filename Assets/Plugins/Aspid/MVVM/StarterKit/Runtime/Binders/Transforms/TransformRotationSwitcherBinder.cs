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
    public sealed class TransformRotationSwitcherBinder : SwitcherBinder<Transform, Vector3>
    {
        [SerializeField] private Space _space;
        
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter? _converter;
        
        public TransformRotationSwitcherBinder(
            Transform target, 
            Vector3 trueValue, 
            Vector3 falseValue, 
            Func<Quaternion, Quaternion> converter) 
            : this(target, trueValue, falseValue, Space.World, converter) { }
        
        public TransformRotationSwitcherBinder(
            Transform target, 
            Vector3 trueValue, 
            Vector3 falseValue, 
            Space space,
            Func<Quaternion, Quaternion> converter) 
            : this(target, trueValue, falseValue, space, converter.ToConvert()) { }
        
        public TransformRotationSwitcherBinder(
            Transform target, 
            Vector3 trueValue, 
            Vector3 falseValue, 
            Converter? converter) 
            : this(target, trueValue, falseValue, Space.World, converter) { }

        public TransformRotationSwitcherBinder(
            Transform target, 
            Vector3 trueValue, 
            Vector3 falseValue, 
            Space space = Space.World,
            Converter? converter = null) 
            : base(target, trueValue, falseValue)
        {
            _space = space;
            _converter = converter; 
        }

        protected override void SetValue(Vector3 value) 
        {
            var rotation = Quaternion.Euler(value);
            rotation = _converter?.Convert(rotation) ?? rotation;
            
            Target.SetRotation(rotation, _space);
        }
    }
}