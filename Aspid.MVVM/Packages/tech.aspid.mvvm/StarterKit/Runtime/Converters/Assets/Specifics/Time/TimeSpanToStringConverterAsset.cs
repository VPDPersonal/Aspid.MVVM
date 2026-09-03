#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="TimeSpan"/> to <see cref="string"/> conversions.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Time/Time Span To String Converter", fileName = "TimeSpanToStringConverter")]
    public sealed class TimeSpanToStringConverterAsset : ConverterAsset<TimeSpan, string?> { }
}
