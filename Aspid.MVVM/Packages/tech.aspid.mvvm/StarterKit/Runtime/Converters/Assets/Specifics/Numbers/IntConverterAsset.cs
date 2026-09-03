using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="int"/> values.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Numbers/Int Converter", fileName = "IntConverter")]
    public sealed class IntConverterAsset : ConverterAsset<int, int> { }
}
