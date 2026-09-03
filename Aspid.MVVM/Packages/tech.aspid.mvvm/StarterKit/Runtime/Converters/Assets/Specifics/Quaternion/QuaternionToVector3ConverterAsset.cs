using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="Quaternion"/> to <see cref="Vector3"/> conversions.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Quaternion/Quaternion To Vector3 Converter", fileName = "QuaternionToVector3Converter")]
    public sealed class QuaternionToVector3ConverterAsset : ConverterAsset<Quaternion, Vector3> { }
}
