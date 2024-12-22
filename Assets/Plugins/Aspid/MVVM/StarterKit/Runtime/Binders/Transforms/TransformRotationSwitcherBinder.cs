#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public sealed class TransformRotationSwitcherBinder : SwitcherBinder<Vector3>
    {
        [SerializeField] private Space _space;
        
        [Header("Component")]
        [SerializeField] private Transform _transform;

#if UNITY_2023_1_OR_NEWER
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#endif
        private IConverter<Quaternion, Quaternion>? _converter;

        public TransformRotationSwitcherBinder(
            Vector3 trueValue, 
            Vector3 falseValue, 
            Transform transform, 
            Space space = Space.World,
            IConverter<Quaternion, Quaternion>? converter = null) 
            : base(trueValue, falseValue)
        {
            _space = space;
            _converter = converter;
            _transform = transform ?? throw new ArgumentNullException(nameof(transform));
        }

        protected override void SetValue(Vector3 value) 
        {
            var rotation = Quaternion.Euler(value);
            rotation = _converter?.Convert(rotation) ?? rotation;
            
            _transform.SetRotation(rotation, _space);
        }
    }
}