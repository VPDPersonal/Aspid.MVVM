#nullable enable
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="int"/> to <see cref="RectOffset"/> conversions.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Numbers/Int To Rect Offset Converter", fileName = "IntToRectOffsetConverter")]
    public sealed class IntToRectOffsetConverterAsset : ConverterAsset<int, RectOffset?> { }
}
