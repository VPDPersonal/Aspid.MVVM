#nullable enable
using TMPro;
using System;
using UnityEngine;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> base for a concrete enum type turned into dropdown
    /// options. Unity cannot create an asset of an open generic, so subclass with
    /// <typeparamref name="T"/> closed.
    /// </summary>
    /// <typeparam name="T">The enum type the converter works over.</typeparam>
    public abstract class EnumToDropdownOptionDataConverterAsset<T> : ConverterAsset<T, IEnumerable<TMP_Dropdown.OptionData>?>
        where T : struct, Enum { }

    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for boxed <see cref="Enum"/> values turned into
    /// dropdown options.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Enum/Enum To Dropdown Options Converter", fileName = "EnumToDropdownOptionDataConverter")]
    public sealed class EnumToDropdownOptionDataConverterAsset : ConverterAsset<Enum?, IEnumerable<TMP_Dropdown.OptionData>?> { }
}
