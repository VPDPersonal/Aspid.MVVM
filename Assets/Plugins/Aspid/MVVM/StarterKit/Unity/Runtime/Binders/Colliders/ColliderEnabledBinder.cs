#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public class ColliderEnabledBinder : TargetBinder<Collider>, IBinder<bool>
    {
        [SerializeField] private bool _isInvert;

        public ColliderEnabledBinder(Collider target, BindMode mode)
            : this(target, false, mode) { }
        
        public ColliderEnabledBinder(Collider target, bool isInvert, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            _isInvert = isInvert;
        }

        public void SetValue(bool value) =>
            Target.enabled = _isInvert ? !value : value;
    }
}