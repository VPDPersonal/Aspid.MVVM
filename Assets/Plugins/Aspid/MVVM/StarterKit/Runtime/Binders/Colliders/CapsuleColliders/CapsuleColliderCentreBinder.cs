#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public sealed class CapsuleColliderCentreBinder : Binder, IVectorBinder, INumberBinder
    {
        [Header("Component")]
        [SerializeField] private CapsuleCollider _collider;
        
        // ReSharper disable once MemberInitializerValueIgnored
        [Header("Converter")]
        [SerializeField] private Vector3CombineConverter? _converter = Vector3CombineConverter.Default;

        public CapsuleColliderCentreBinder(CapsuleCollider collider, Vector3CombineConverter? converter = null)
        {
            _converter = converter;
            _collider = collider ?? throw new ArgumentNullException(nameof(collider));
        }
        
        public void SetValue(Vector2 value) =>
            SetValue((Vector3)value);
        
        public void SetValue(Vector3 value) =>
            _collider.center = _converter?.Convert(value, _collider.center) ?? value;
        
        public void SetValue(int value) =>
            SetValue(new Vector3(value, value, value));
        
        public void SetValue(long value) =>
            SetValue(new Vector3(value, value, value));
        
        public void SetValue(float value) =>
            SetValue(new Vector3(value, value, value));
        
        public void SetValue(double value) =>
            SetValue((float)value);
    }
}