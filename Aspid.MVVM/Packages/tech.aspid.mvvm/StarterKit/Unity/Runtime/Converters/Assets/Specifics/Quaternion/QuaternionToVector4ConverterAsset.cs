#nullable enable
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="Quaternion"/> to <see cref="Vector4"/> conversions.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Quaternion/Quaternion To Vector4 Converter", fileName = "QuaternionToVector4Converter")]
    public sealed class QuaternionToVector4ConverterAsset : ConverterAsset<Quaternion, Vector4> { }
}
