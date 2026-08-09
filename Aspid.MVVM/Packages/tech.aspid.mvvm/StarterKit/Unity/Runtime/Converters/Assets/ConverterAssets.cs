#nullable enable
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="string"/> values.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/String Converter", fileName = "StringConverter")]
    public sealed class StringConverterAsset : ConverterAsset<string?, string?> { }

    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="float"/> values.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Float Converter", fileName = "FloatConverter")]
    public sealed class FloatConverterAsset : ConverterAsset<float, float> { }

    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="int"/> values.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Int Converter", fileName = "IntConverter")]
    public sealed class IntConverterAsset : ConverterAsset<int, int> { }

    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="bool"/> values.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Bool Converter", fileName = "BoolConverter")]
    public sealed class BoolConverterAsset : ConverterAsset<bool, bool> { }

    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="Color"/> values.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Color Converter", fileName = "ColorConverter")]
    public sealed class ColorConverterAsset : ConverterAsset<Color, Color> { }

    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="Vector2"/> values.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Vector2 Converter", fileName = "Vector2Converter")]
    public sealed class Vector2ConverterAsset : ConverterAsset<Vector2, Vector2> { }

    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="Vector3"/> values.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Vector3 Converter", fileName = "Vector3Converter")]
    public sealed class Vector3ConverterAsset : ConverterAsset<Vector3, Vector3> { }

    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> that renders any value as text.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Object To String Converter", fileName = "ObjectToStringConverter")]
    public sealed class ObjectToStringConverterAsset : ConverterAsset<object?, string?> { }
}
