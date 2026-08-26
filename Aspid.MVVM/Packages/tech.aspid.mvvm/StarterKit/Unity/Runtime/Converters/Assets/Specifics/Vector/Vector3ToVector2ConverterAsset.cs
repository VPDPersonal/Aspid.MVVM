#nullable enable
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="Vector3"/> to <see cref="Vector2"/> conversions.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Vector/Vector3 To Vector2 Converter", fileName = "Vector3ToVector2Converter")]
    public sealed class Vector3ToVector2ConverterAsset : ConverterAsset<Vector3, Vector2> { }
}
