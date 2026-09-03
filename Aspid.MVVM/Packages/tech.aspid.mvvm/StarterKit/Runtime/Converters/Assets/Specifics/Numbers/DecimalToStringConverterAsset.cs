#nullable enable
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="decimal"/> to <see cref="string"/> conversions.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Numbers/Decimal To String Converter", fileName = "DecimalToStringConverter")]
    public sealed class DecimalToStringConverterAsset : ConverterAsset<decimal, string?> { }
}
