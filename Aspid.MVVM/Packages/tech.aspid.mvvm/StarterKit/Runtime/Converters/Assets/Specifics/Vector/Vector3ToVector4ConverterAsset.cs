using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="Vector3"/> to <see cref="Vector4"/> conversions.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Vector/Vector3 To Vector4 Converter", fileName = "Vector3ToVector4Converter")]
    public sealed class Vector3ToVector4ConverterAsset : ConverterAsset<Vector3, Vector4> { }
}
