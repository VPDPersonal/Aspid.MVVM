#nullable enable
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="Vector3"/> values.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Vector3 Converter", fileName = "Vector3Converter")]
    public sealed class Vector3ConverterAsset : ConverterAsset<Vector3, Vector3> { }
}
