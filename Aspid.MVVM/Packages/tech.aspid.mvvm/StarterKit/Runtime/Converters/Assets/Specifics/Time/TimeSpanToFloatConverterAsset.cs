using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="TimeSpan"/> to <see cref="float"/> conversions.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Time/Time Span To Float Converter", fileName = "TimeSpanToFloatConverter")]
    public sealed class TimeSpanToFloatConverterAsset : ConverterAsset<TimeSpan, float> { }
}
