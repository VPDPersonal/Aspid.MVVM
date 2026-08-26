#nullable enable
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="Vector2"/> to <see cref="float"/> conversions.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Vector/Vector2 To Float Converter", fileName = "Vector2ToFloatConverter")]
    public sealed class Vector2ToFloatConverterAsset : ConverterAsset<Vector2, float> { }
}
