#nullable enable
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="Vector4"/> to <see cref="Vector3"/> conversions.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Vector/Vector4 To Vector3 Converter", fileName = "Vector4ToVector3Converter")]
    public sealed class Vector4ToVector3ConverterAsset : ConverterAsset<Vector4, Vector3> { }
}
