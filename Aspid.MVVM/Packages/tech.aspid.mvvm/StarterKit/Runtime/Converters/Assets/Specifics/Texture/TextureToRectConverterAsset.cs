#nullable enable
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="Texture"/> to <see cref="Rect"/> conversions.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Texture/Texture To Rect Converter", fileName = "TextureToRectConverter")]
    public sealed class TextureToRectConverterAsset : ConverterAsset<Texture?, Rect> { }
}
