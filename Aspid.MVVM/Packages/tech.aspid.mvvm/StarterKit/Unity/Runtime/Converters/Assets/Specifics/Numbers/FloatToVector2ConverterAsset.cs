#nullable enable
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="float"/> to <see cref="Vector2"/> conversions.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Numbers/Float To Vector2 Converter", fileName = "FloatToVector2Converter")]
    public sealed class FloatToVector2ConverterAsset : ConverterAsset<float, Vector2> { }
}
