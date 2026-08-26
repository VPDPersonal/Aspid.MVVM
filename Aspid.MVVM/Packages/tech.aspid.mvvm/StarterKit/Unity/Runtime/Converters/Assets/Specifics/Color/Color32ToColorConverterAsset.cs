#nullable enable
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="Color32"/> to <see cref="Color"/> conversions.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Color/Color32 To Color Converter", fileName = "Color32ToColorConverter")]
    public sealed class Color32ToColorConverterAsset : ConverterAsset<Color32, Color> { }
}
