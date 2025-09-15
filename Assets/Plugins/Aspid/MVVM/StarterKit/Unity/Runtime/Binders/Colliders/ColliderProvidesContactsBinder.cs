#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public class ColliderProvidesContactsBinder : TargetBinder<Collider>, IBinder<bool>
    {
        [Header("Converter")]
        [SerializeField] private bool _isInvert;

        public ColliderProvidesContactsBinder(Collider target, BindMode mode)
            : this(target, false, mode) { }
        
        public ColliderProvidesContactsBinder(Collider target, bool isInvert = false, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            _isInvert = isInvert;
        }

        public void SetValue(bool value) =>
            Target.providesContacts = _isInvert ? !value : value;
    }
}