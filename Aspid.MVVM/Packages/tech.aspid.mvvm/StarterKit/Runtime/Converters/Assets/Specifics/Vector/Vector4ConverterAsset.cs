using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="Vector4"/> values.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Vector/Vector4 Converter", fileName = "Vector4Converter")]
    public sealed class Vector4ConverterAsset : ConverterAsset<Vector4, Vector4> { }
}
