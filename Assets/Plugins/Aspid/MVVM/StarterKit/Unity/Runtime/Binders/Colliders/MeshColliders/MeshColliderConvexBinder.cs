#nullable enable
using System;
using UnityEngine;

namespace Aspid.MVVM.StarterKit.Unity
{
    [Serializable]
    public class MeshColliderConvexBinder : TargetBinder<MeshCollider>, IBinder<bool>
    {
        [Header("Converter")]
        [SerializeField] private bool _isInvert;

        public MeshColliderConvexBinder(MeshCollider target, BindMode bindMode)
            : this(target, false, bindMode) { }
        
        public MeshColliderConvexBinder(MeshCollider target, bool isInvert = false, BindMode bindMode = BindMode.OneWay)
            : base(target, bindMode)
        {
            Mode.ThrowExceptionIfTwo();
            _isInvert = isInvert;
        }

        public void SetValue(bool value) =>
            Target.convex = _isInvert ? !value : value;
    }
}