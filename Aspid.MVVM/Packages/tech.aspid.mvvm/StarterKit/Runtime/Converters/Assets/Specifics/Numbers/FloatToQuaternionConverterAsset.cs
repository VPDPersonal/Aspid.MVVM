using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="float"/> to <see cref="Quaternion"/> conversions.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Numbers/Float To Quaternion Converter", fileName = "FloatToQuaternionConverter")]
    public sealed class FloatToQuaternionConverterAsset : ConverterAsset<float, Quaternion> { }
}
