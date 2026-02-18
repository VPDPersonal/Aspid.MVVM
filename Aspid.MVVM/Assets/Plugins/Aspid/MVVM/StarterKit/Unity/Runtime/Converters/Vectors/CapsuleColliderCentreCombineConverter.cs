using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public sealed class CapsuleColliderCentreCombineConverter : Vector3CombineConverter
    {
        [SerializeField] private CapsuleCollider _collider;
        
        protected override Vector3 VectorTo => _collider.center;
    }
}
