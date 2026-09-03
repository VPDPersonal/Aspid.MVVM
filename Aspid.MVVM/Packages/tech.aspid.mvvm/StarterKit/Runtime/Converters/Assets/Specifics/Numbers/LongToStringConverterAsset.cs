#nullable enable
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="long"/> to <see cref="string"/> conversions.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Numbers/Long To String Converter", fileName = "LongToStringConverter")]
    public sealed class LongToStringConverterAsset : ConverterAsset<long, string?> { }
}
