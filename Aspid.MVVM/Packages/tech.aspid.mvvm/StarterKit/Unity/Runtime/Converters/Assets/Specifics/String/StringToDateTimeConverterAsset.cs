#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="string"/> to <see cref="DateTime"/> conversions.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/String/String To Date Time Converter", fileName = "StringToDateTimeConverter")]
    public sealed class StringToDateTimeConverterAsset : ConverterAsset<string?, DateTime> { }
}
