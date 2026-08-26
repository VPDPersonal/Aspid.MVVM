#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="DateTime"/> to <see cref="string"/> conversions.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Time/Date Time To String Converter", fileName = "DateTimeToStringConverter")]
    public sealed class DateTimeToStringConverterAsset : ConverterAsset<DateTime, string?> { }
}
