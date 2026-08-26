#nullable enable
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="Color"/> to <see cref="ColorBlock"/> conversions.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Color/Color To Color Block Converter", fileName = "ColorToColorBlockConverter")]
    public sealed class ColorToColorBlockConverterAsset : ConverterAsset<Color, ColorBlock> { }
}
