#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="string"/> to <see cref="TimeSpan"/> conversions.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/String/String To Time Span Converter", fileName = "StringToTimeSpanConverter")]
    public sealed class StringToTimeSpanConverterAsset : ConverterAsset<string?, TimeSpan> { }
}
