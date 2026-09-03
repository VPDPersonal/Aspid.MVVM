#nullable enable
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="RectOffset"/> values.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Rect/Rect Offset Converter", fileName = "RectOffsetConverter")]
    public sealed class RectOffsetConverterAsset : ConverterAsset<RectOffset?, RectOffset?> { }
}
