#nullable enable
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="float"/> to <see cref="Sprite"/> conversions.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Numbers/Float To Sprite Converter", fileName = "FloatToSpriteConverter")]
    public sealed class FloatToSpriteConverterAsset : ConverterAsset<float, Sprite?> { }
}
