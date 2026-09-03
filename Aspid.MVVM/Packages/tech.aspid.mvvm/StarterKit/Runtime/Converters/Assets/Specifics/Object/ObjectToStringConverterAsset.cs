#nullable enable
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="object"/> to <see cref="string"/> conversions.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Object/Object To String Converter", fileName = "ObjectToStringConverter")]
    public sealed class ObjectToStringConverterAsset : ConverterAsset<object?, string?> { }
}
