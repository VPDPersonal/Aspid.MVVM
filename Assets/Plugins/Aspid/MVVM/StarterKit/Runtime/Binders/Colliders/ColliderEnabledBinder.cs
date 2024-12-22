#nullable enable
using System;
using UnityEngine;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class ColliderEnabledBinder : Binder, IBinder<bool>
    {
        [Header("Component")]
        [SerializeField] private Collider _collider;

        [Header("Converter")]
        [SerializeField] private bool _isInvert;

        public ColliderEnabledBinder(Collider collider, bool isInvert)
        {
            _isInvert = isInvert;
            _collider = collider ?? throw new ArgumentNullException(nameof(collider));
        }

        public void SetValue(bool value) =>
            _collider.enabled = _isInvert ? !value : value;
    }
}