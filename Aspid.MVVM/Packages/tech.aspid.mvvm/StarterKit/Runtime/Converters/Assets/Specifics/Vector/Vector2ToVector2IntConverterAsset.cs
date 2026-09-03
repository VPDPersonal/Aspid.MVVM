using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="Vector2"/> to <see cref="Vector2Int"/> conversions.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Vector/Vector2 To Vector2 Int Converter", fileName = "Vector2ToVector2IntConverter")]
    public sealed class Vector2ToVector2IntConverterAsset : ConverterAsset<Vector2, Vector2Int> { }
}
