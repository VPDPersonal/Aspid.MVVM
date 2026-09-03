#nullable enable
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="Color"/> to <see cref="string"/> conversions.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Color/Color To String Converter", fileName = "ColorToStringConverter")]
    public sealed class ColorToStringConverterAsset : ConverterAsset<Color, string?> { }
}
