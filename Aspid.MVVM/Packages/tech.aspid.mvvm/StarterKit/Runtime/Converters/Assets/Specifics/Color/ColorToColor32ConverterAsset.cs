using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="Color"/> to <see cref="Color32"/> conversions.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Color/Color To Color32 Converter", fileName = "ColorToColor32Converter")]
    public sealed class ColorToColor32ConverterAsset : ConverterAsset<Color, Color32> { }
}
