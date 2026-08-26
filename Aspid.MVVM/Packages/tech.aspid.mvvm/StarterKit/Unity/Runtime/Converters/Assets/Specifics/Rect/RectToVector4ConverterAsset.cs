#nullable enable
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="Rect"/> to <see cref="Vector4"/> conversions.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Rect/Rect To Vector4 Converter", fileName = "RectToVector4Converter")]
    public sealed class RectToVector4ConverterAsset : ConverterAsset<Rect, Vector4> { }
}
