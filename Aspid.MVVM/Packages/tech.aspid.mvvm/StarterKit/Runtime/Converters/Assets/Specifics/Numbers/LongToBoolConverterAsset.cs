using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="long"/> to <see cref="bool"/> conversions.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Numbers/Long To Bool Converter", fileName = "LongToBoolConverter")]
    public sealed class LongToBoolConverterAsset : ConverterAsset<long, bool> { }
}
