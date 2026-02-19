using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public sealed class RectTransformAnchoredPositionCombineConverter : Vector3CombineConverter
    {
        [SerializeField] private RectTransform _transform;
        [SerializeField] private Space _space = Space.World;

        protected override Vector3 VectorTo => _transform.GetAnchoredPosition(_space);
    }
}