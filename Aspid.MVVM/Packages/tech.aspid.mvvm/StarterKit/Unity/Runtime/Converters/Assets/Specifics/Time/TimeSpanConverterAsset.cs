#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="TimeSpan"/> values.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Time/Time Span Converter", fileName = "TimeSpanConverter")]
    public sealed class TimeSpanConverterAsset : ConverterAsset<TimeSpan, TimeSpan> { }
}
