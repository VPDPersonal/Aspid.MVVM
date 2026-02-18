using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public sealed class TransformEulerAnglesCombineConverter : Vector3CombineConverter
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private Space _space = Space.World;

        protected override Vector3 VectorTo => _transform.GetEulerAngles(_space);
    }
}
