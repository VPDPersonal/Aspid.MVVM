using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="Bounds"/> values.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Bounds/Bounds Converter", fileName = "BoundsConverter")]
    public sealed class BoundsConverterAsset : ConverterAsset<Bounds, Bounds> { }
}
