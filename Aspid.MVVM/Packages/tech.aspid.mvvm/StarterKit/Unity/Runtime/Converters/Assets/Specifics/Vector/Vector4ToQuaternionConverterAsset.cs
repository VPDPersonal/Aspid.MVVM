#nullable enable
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="Vector4"/> to <see cref="Quaternion"/> conversions.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Vector/Vector4 To Quaternion Converter", fileName = "Vector4ToQuaternionConverter")]
    public sealed class Vector4ToQuaternionConverterAsset : ConverterAsset<Vector4, Quaternion> { }
}
