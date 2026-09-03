using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="Vector2"/> to <see cref="Vector3"/> conversions.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Vector/Vector2 To Vector3 Converter", fileName = "Vector2ToVector3Converter")]
    public sealed class Vector2ToVector3ConverterAsset : ConverterAsset<Vector2, Vector3> { }
}
