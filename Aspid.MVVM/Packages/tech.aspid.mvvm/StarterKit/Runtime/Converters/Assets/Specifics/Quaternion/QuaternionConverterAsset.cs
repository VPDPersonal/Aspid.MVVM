using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="Quaternion"/> values.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Quaternion/Quaternion Converter", fileName = "QuaternionConverter")]
    public sealed class QuaternionConverterAsset : ConverterAsset<Quaternion, Quaternion> { }
}
