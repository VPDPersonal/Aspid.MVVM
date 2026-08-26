#nullable enable
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="Vector4"/> to <see cref="RectOffset"/> conversions.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Vector/Vector4 To Rect Offset Converter", fileName = "Vector4ToRectOffsetConverter")]
    public sealed class Vector4ToRectOffsetConverterAsset : ConverterAsset<Vector4, RectOffset?> { }
}
