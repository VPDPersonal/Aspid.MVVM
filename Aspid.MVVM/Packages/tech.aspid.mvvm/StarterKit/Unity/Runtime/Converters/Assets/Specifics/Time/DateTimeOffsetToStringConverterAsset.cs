#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="DateTimeOffset"/> to <see cref="string"/> conversions.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Time/Date Time Offset To String Converter", fileName = "DateTimeOffsetToStringConverter")]
    public sealed class DateTimeOffsetToStringConverterAsset : ConverterAsset<DateTimeOffset, string?> { }
}
