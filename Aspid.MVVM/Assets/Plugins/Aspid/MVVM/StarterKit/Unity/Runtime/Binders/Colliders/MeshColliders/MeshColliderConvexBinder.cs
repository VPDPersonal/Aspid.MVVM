#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public class MeshColliderConvexBinder : TargetBoolBinder<MeshCollider>
    {
        protected sealed override bool Property
        {
            get => Target.convex;
            set => Target.convex = value;
        }
        
        public MeshColliderConvexBinder(MeshCollider target, BindMode bindMode)
            : this(target, isInvert: false, bindMode) { }
        
        public MeshColliderConvexBinder(MeshCollider target, bool isInvert = false, BindMode bindMode = BindMode.OneWay)
            : base(target, isInvert, bindMode)
        {
            Mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}