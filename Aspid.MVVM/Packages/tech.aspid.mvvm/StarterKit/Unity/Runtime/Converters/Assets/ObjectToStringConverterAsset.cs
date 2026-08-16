#nullable enable
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> that renders any value as text.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Object To String Converter", fileName = "ObjectToStringConverter")]
    public sealed class ObjectToStringConverterAsset : ConverterAsset<object?, string?> { }
}
