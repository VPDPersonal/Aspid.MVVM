#nullable enable
using System;
using UnityEngine;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class ColliderIsTriggerBinder : TargetBinder<Collider>, IBinder<bool>
    {
        [Header("Converter")]
        [SerializeField] private bool _isInvert;

        public ColliderIsTriggerBinder(Collider target, BindMode mode)
            : this(target, false, mode) { }
        
        public ColliderIsTriggerBinder(Collider target, bool isInvert, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            _isInvert = isInvert;
        }

        public void SetValue(bool value) =>
            Target.isTrigger = _isInvert ? !value : value;
    }
}