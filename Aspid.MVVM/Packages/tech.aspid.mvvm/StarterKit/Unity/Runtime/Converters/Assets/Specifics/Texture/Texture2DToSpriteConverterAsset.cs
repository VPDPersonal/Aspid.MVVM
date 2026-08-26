#nullable enable
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="Texture2D"/> to <see cref="Sprite"/> conversions.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Texture/Texture2D To Sprite Converter", fileName = "Texture2DToSpriteConverter")]
    public sealed class Texture2DToSpriteConverterAsset : ConverterAsset<Texture2D?, Sprite?> { }
}
