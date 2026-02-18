using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public sealed class BoxColliderSizeCombineConverter : Vector3CombineConverter
    {
        [SerializeField] private BoxCollider _collider;
        
        protected override Vector3 VectorTo => _collider.size;
    }
}
