using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="Bounds"/> to <see cref="Vector3"/> conversions.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Bounds/Bounds To Vector3 Converter", fileName = "BoundsToVector3Converter")]
    public sealed class BoundsToVector3ConverterAsset : ConverterAsset<Bounds, Vector3> { }
}
