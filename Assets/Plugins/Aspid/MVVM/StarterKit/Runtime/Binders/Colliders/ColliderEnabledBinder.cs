#nullable enable
using System;
using UnityEngine;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class ColliderEnabledBinder : TargetBinder<Collider>, IBinder<bool>
    {
        [Header("Converter")]
        [SerializeField] private bool _isInvert;

        public ColliderEnabledBinder(Collider target, bool isInvert)
            : base(target)
        {
            _isInvert = isInvert;
        }

        public void SetValue(bool value) =>
            Target.enabled = _isInvert ? !value : value;
    }
}