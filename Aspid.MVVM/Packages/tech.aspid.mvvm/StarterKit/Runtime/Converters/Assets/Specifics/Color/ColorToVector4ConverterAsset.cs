using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="Color"/> to <see cref="Vector4"/> conversions.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Color/Color To Vector4 Converter", fileName = "ColorToVector4Converter")]
    public sealed class ColorToVector4ConverterAsset : ConverterAsset<Color, Vector4> { }
}
