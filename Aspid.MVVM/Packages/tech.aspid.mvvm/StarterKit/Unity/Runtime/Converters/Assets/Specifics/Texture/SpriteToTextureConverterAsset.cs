#nullable enable
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="Sprite"/> to <see cref="Texture"/> conversions.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Texture/Sprite To Texture Converter", fileName = "SpriteToTextureConverter")]
    public sealed class SpriteToTextureConverterAsset : ConverterAsset<Sprite?, Texture?> { }
}
