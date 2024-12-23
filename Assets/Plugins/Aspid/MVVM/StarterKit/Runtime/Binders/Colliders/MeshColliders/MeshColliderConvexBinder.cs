#nullable enable
using System;
using UnityEngine;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class MeshColliderConvexBinder : TargetBinder<MeshCollider>, IBinder<bool>
    {
        [Header("Converter")]
        [SerializeField] private bool _isInvert;

        public MeshColliderConvexBinder(MeshCollider target, bool isInvert = false)
            : base(target)
        {
            _isInvert = isInvert;
        }

        public void SetValue(bool value) =>
            Target.convex = _isInvert ? !value : value;
    }
}