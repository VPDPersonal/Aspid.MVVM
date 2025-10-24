#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public class SphereColliderCenterBinder : TargetBinder<SphereCollider>, IVectorBinder, INumberBinder
    {
        // ReSharper disable once MemberInitializerValueIgnored
        [SerializeField] private Vector3CombineConverter? _converter = Vector3CombineConverter.Default;

        public SphereColliderCenterBinder(SphereCollider target, BindMode mode)
            : this(target, null, mode) { }
        
        public SphereColliderCenterBinder(
            SphereCollider target,
            Vector3CombineConverter? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            _converter = converter;
        }
        
        public void SetValue(Vector2 value) =>
            SetValue((Vector3)value);
        
        public void SetValue(Vector3 value) =>
            Target.center = _converter?.Convert(value, Target.center) ?? value;
        
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