#nullable enable
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="Vector4"/> to <see cref="Rect"/> conversions.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Vector/Vector4 To Rect Converter", fileName = "Vector4ToRectConverter")]
    public sealed class Vector4ToRectConverterAsset : ConverterAsset<Vector4, Rect> { }
}
