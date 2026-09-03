#nullable enable
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="string"/> to <see cref="double"/> conversions.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/String/String To Double Converter", fileName = "StringToDoubleConverter")]
    public sealed class StringToDoubleConverterAsset : ConverterAsset<string?, double> { }
}
