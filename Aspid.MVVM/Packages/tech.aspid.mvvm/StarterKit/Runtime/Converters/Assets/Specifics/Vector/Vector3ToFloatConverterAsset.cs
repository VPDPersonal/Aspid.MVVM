using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="Vector3"/> to <see cref="float"/> conversions.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Vector/Vector3 To Float Converter", fileName = "Vector3ToFloatConverter")]
    public sealed class Vector3ToFloatConverterAsset : ConverterAsset<Vector3, float> { }
}
