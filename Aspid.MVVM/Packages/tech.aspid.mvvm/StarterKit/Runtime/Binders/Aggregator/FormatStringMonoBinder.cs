using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="AggregatorMonoBinder{TInput, TResult}"/> that formats the input strings into one line.
    /// </summary>
    /// <remarks>
    /// A <see langword="null"/> input formats as an empty string; a format that does not match the inputs is reported.
    /// </remarks>
    [AddComponentMenu("Aspid/MVVM/Binders/Aggregator/Aggregator – Format String")]
    public sealed class FormatStringMonoBinder : AggregatorMonoBinder<string, string>
    {
        [Tooltip("Composite format, one placeholder per input, e.g. {0} / {1}.")]
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
                BinderLogger.LogError(
                    GetType(),
                    exception,
                    consequence: $"The format {_format.Describe()} does not match {values.Length} inputs, " +
                        "so an empty string is combined.",
                    context: this);

                return string.Empty;
            }
        }
    }
}
