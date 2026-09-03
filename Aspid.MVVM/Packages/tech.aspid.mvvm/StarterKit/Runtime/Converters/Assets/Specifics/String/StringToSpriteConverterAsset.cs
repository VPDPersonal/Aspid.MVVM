#nullable enable
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="string"/> to <see cref="Sprite"/> conversions.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/String/String To Sprite Converter", fileName = "StringToSpriteConverter")]
    public sealed class StringToSpriteConverterAsset : ConverterAsset<string?, Sprite?> { }
}
