#nullable enable
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="Vector3"/> to <see cref="Quaternion"/> conversions.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Vector/Vector3 To Quaternion Converter", fileName = "Vector3ToQuaternionConverter")]
    public sealed class Vector3ToQuaternionConverterAsset : ConverterAsset<Vector3, Quaternion> { }
}
