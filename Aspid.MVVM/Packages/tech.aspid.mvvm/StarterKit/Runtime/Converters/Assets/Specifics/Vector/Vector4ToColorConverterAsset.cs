using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="Vector4"/> to <see cref="Color"/> conversions.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Vector/Vector4 To Color Converter", fileName = "Vector4ToColorConverter")]
    public sealed class Vector4ToColorConverterAsset : ConverterAsset<Vector4, Color> { }
}
