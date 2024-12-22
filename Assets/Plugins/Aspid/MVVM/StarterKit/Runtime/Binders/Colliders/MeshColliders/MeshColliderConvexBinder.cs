#nullable enable
using System;
using UnityEngine;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class MeshColliderConvexBinder : Binder, IBinder<bool>
    {
        [Header("Component")]
        [SerializeField] private MeshCollider _collider;
        
        [Header("Converter")]
        [SerializeField] private bool _isInvert;

        public MeshColliderConvexBinder(MeshCollider collider, bool isInvert = false)
        {
            _isInvert = isInvert;
            _collider = collider ?? throw new ArgumentNullException(nameof(collider));
        }

        public void SetValue(bool value) =>
            _collider.convex = _isInvert ? !value : value;
    }
}