using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="long"/> to <see cref="DateTime"/> conversions.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Numbers/Long To Date Time Converter", fileName = "LongToDateTimeConverter")]
    public sealed class LongToDateTimeConverterAsset : ConverterAsset<long, DateTime> { }
}
