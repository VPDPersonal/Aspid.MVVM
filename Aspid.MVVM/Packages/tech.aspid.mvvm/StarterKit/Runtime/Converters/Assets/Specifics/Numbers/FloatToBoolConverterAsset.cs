using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="float"/> to <see cref="bool"/> conversions.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Numbers/Float To Bool Converter", fileName = "FloatToBoolConverter")]
    public sealed class FloatToBoolConverterAsset : ConverterAsset<float, bool> { }
}
