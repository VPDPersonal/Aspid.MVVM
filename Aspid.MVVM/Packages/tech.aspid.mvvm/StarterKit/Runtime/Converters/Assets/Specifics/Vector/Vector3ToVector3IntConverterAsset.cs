using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="Vector3"/> to <see cref="Vector3Int"/> conversions.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Vector/Vector3 To Vector3 Int Converter", fileName = "Vector3ToVector3IntConverter")]
    public sealed class Vector3ToVector3IntConverterAsset : ConverterAsset<Vector3, Vector3Int> { }
}
