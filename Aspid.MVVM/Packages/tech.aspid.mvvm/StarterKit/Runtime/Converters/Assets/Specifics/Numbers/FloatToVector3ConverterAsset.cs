using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="float"/> to <see cref="Vector3"/> conversions.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Numbers/Float To Vector3 Converter", fileName = "FloatToVector3Converter")]
    public sealed class FloatToVector3ConverterAsset : ConverterAsset<float, Vector3> { }
}
