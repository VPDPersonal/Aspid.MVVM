using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public sealed class TransformScaleCombineConverter : Vector3CombineConverter
    {
        [SerializeField] private Transform _transform;

        protected override Vector3 VectorTo => _transform.localScale;
    }
}
