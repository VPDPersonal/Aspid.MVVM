#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public sealed class CapsuleColliderRadiusBinder : Binder, INumberBinder
    {
        [Header("Component")]
        [SerializeField] private CapsuleCollider _collider;
        
#if UNITY_2023_1_OR_NEWER
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#endif
        private IConverter<float, float>? _converter;

        public CapsuleColliderRadiusBinder(CapsuleCollider collider, Func<float, float> converter)
            : this(collider, converter.ToConvert()) { }
        
        public CapsuleColliderRadiusBinder(CapsuleCollider collider, IConverter<float, float>? converter = null)
        {
            _converter = converter;
            _collider = collider ?? throw new ArgumentNullException(nameof(collider));
        }

        public void SetValue(int value) =>
            SetValue((float)value);

        public void SetValue(long value) =>
            SetValue((float)value);
        
        public void SetValue(float value) =>
            _collider.radius = _converter?.Convert(value) ?? value;

        public void SetValue(double value) =>
            SetValue((float)value);
    }
}