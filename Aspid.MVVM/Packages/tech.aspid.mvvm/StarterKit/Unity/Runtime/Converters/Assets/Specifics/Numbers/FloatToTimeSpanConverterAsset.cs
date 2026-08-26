#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="float"/> to <see cref="TimeSpan"/> conversions.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Numbers/Float To Time Span Converter", fileName = "FloatToTimeSpanConverter")]
    public sealed class FloatToTimeSpanConverterAsset : ConverterAsset<float, TimeSpan> { }
}
