#nullable enable
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="Quaternion"/> to <see cref="float"/> conversions.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Quaternion/Quaternion To Float Converter", fileName = "QuaternionToFloatConverter")]
    public sealed class QuaternionToFloatConverterAsset : ConverterAsset<Quaternion, float> { }
}
