using System;
using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Vector3, UnityEngine.Vector3>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterVector3;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public sealed class TransformPositionCombineConverter : Vector3CombineConverter
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private Space _space = Space.World;

        protected override Vector3 VectorTo => _transform.GetPosition(_space);
    }
}