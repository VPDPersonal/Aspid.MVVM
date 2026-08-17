#nullable enable
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="Vector2"/> values.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Vector2 Converter", fileName = "Vector2Converter")]
    public sealed class Vector2ConverterAsset : ConverterAsset<Vector2, Vector2> { }
}
