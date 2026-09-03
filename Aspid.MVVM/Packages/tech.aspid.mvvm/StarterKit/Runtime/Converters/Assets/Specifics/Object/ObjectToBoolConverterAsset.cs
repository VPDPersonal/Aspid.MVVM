#nullable enable
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="object"/> to <see cref="bool"/> conversions.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Object/Object To Bool Converter", fileName = "ObjectToBoolConverter")]
    public sealed class ObjectToBoolConverterAsset : ConverterAsset<object?, bool> { }
}
