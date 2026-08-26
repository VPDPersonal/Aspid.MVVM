#nullable enable
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="ColorBlock"/> values.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Color/Color Block Converter", fileName = "ColorBlockConverter")]
    public sealed class ColorBlockConverterAsset : ConverterAsset<ColorBlock, ColorBlock> { }
}
