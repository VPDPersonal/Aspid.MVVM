using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Concrete <see cref="AggregatorMonoBinder{T1, T2}">AggregatorMonoBinder&lt;string, string&gt;</see> that composes
    /// several bound strings into one line.
    /// </summary>
    /// <remarks>
    /// A <see langword="null"/> input is composed as an empty string. A format string that does not match the inputs is
    /// logged rather than thrown.
    /// </remarks>
    [AddComponentMenu("Aspid/MVVM/Binders/Aggregator/Aggregator – Format String")]
    public sealed class FormatStringMonoBinder : AggregatorMonoBinder<string, string>
    {
        [Tooltip("Composite format string, with one placeholder per input, for example: {0} / {1}")]
        [SerializeField] private string _format = "{0} {1}";

        /// <inheritdoc/>
        protected override string Combine(string[] values)
        {
            var parts = new object[values.Length];

            for (var i = 0; i < values.Length; i++)
                parts[i] = values[i] ?? string.Empty;

            try
            {
                return string.Format(_format ?? string.Empty, parts);
            }
            catch (FormatException exception)
            {
                BinderLogger.LogError(GetType(), exception, $"The format {_format.Describe()} does not match {values.Length} inputs, so an empty string is combined.", this);
                return string.Empty;
            }
        }
    }
}
