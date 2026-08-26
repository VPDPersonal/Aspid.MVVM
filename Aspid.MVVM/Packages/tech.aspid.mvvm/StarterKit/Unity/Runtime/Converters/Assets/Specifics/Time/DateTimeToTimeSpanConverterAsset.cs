#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="DateTime"/> to <see cref="TimeSpan"/> conversions.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Time/Date Time To Time Span Converter", fileName = "DateTimeToTimeSpanConverter")]
    public sealed class DateTimeToTimeSpanConverterAsset : ConverterAsset<DateTime, TimeSpan> { }
}
