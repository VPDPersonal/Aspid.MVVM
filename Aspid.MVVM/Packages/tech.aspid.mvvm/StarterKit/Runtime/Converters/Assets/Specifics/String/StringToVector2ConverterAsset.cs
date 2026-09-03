#nullable enable
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="string"/> to <see cref="Vector2"/> conversions.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/String/String To Vector2 Converter", fileName = "StringToVector2Converter")]
    public sealed class StringToVector2ConverterAsset : ConverterAsset<string?, Vector2> { }
}
