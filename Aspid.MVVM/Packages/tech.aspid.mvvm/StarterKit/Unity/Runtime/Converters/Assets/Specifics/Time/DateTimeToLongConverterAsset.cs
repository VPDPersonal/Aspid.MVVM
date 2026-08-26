#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="DateTime"/> to <see cref="long"/> conversions.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Time/Date Time To Long Converter", fileName = "DateTimeToLongConverter")]
    public sealed class DateTimeToLongConverterAsset : ConverterAsset<DateTime, long> { }
}
