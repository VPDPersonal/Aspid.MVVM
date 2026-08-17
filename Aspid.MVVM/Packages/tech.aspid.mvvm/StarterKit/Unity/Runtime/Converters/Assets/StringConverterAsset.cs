#nullable enable
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="string"/> values.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/String Converter", fileName = "StringConverter")]
    public sealed class StringConverterAsset : ConverterAsset<string?, string?> { }
}
