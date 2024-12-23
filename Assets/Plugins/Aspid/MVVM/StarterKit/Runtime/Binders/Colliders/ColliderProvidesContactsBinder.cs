#nullable enable
using System;
using UnityEngine;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class ColliderProvidesContactsBinder : TargetBinder<Collider>, IBinder<bool>
    {
        [Header("Converter")]
        [SerializeField] private bool _isInvert;

        public ColliderProvidesContactsBinder(Collider target, bool isInvert)
            : base(target)
        {
            _isInvert = isInvert;
        }

        public void SetValue(bool value) =>
            Target.providesContacts = _isInvert ? !value : value;
    }
}