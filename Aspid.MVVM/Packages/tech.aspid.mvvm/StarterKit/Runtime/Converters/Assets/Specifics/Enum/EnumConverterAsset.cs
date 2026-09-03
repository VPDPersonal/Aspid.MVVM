#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> base for a concrete enum type. Unity cannot create
    /// an asset of an open generic, so subclass with <typeparamref name="T"/> closed.
    /// </summary>
    /// <typeparam name="T">The enum type the converter works over.</typeparam>
    public abstract class EnumConverterAsset<T> : ConverterAsset<T, T>
        where T : struct, Enum { }

    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for boxed <see cref="Enum"/> values.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Enum/Enum Converter", fileName = "EnumConverter")]
    public sealed class EnumConverterAsset : ConverterAsset<Enum?, Enum?> { }
}
