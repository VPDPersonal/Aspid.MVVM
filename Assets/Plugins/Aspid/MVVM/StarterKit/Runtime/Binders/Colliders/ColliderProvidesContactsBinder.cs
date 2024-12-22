#nullable enable
using System;
using UnityEngine;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class ColliderProvidesContactsBinder : Binder, IBinder<bool>
    {
        [Header("Component")]
        [SerializeField] private Collider _collider;

        [Header("Converter")]
        [SerializeField] private bool _isInvert;

        public ColliderProvidesContactsBinder(Collider collider, bool isInvert)
        {
            _isInvert = isInvert;
            _collider = collider ?? throw new ArgumentNullException(nameof(collider));
        }

        public void SetValue(bool value) =>
            _collider.providesContacts = _isInvert ? !value : value;
    }
}