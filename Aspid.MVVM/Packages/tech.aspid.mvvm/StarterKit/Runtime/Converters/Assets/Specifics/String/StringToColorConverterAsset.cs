#nullable enable
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="string"/> to <see cref="Color"/> conversions.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/String/String To Color Converter", fileName = "StringToColorConverter")]
    public sealed class StringToColorConverterAsset : ConverterAsset<string?, Color> { }
}
