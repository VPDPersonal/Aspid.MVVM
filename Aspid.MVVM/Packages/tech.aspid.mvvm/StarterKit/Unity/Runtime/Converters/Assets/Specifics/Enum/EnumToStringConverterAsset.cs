#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> base for a concrete enum type rendered as text.
    /// Unity cannot create an asset of an open generic, so subclass with <typeparamref name="T"/>
    /// closed.
    /// </summary>
    /// <typeparam name="T">The enum type the converter works over.</typeparam>
    public abstract class EnumToStringConverterAsset<T> : ConverterAsset<T, string?>
        where T : struct, Enum { }

    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for boxed <see cref="Enum"/> values rendered as text.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Enum/Enum To String Converter", fileName = "EnumToStringConverter")]
    public sealed class EnumToStringConverterAsset : ConverterAsset<Enum?, string?> { }
}
