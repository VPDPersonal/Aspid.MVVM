#nullable enable
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="UnityEngine.Object"/> to <see cref="string"/> conversions.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Object/Unity Object To String Converter", fileName = "UnityObjectToStringConverter")]
    public sealed class UnityObjectToStringConverterAsset : ConverterAsset<Object?, string?> { }
}
