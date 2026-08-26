#nullable enable
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="Bounds"/> to <see cref="Rect"/> conversions.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Bounds/Bounds To Rect Converter", fileName = "BoundsToRectConverter")]
    public sealed class BoundsToRectConverterAsset : ConverterAsset<Bounds, Rect> { }
}
