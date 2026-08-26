#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="DateTime"/> to <see cref="bool"/> conversions.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Time/Date Time To Bool Converter", fileName = "DateTimeToBoolConverter")]
    public sealed class DateTimeToBoolConverterAsset : ConverterAsset<DateTime, bool> { }
}
