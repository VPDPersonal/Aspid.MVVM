using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="bool"/> values.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Bool/Bool Converter", fileName = "BoolConverter")]
    public sealed class BoolConverterAsset : ConverterAsset<bool, bool> { }
}
