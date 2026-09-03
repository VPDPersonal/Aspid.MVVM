#nullable enable
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="UnityEngine.Object"/> to <see cref="bool"/> conversions.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Object/Unity Object To Bool Converter", fileName = "UnityObjectToBoolConverter")]
    public sealed class UnityObjectToBoolConverterAsset : ConverterAsset<Object?, bool> { }
}
