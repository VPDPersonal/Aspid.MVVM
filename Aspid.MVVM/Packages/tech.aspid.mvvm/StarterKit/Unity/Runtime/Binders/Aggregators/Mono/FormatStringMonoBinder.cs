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
    /// "Level 7 - Archer", "3 / 12", "Alice (offline)": one label built from values that live in different members. The
    /// alternative is a field in the ViewModel that exists only to hold the concatenation, which puts a piece of the
    /// view's wording into the ViewModel.
    /// <para/>
    /// A <see langword="null"/> input is composed as an empty string, the way
    /// <see cref="string.Format(string, object[])"/> would treat one - a missing name should leave a gap, not the word
    /// <c>null</c>.
    /// <para/>
    /// A format string that does not match the inputs is reported rather than thrown: it is a configuration mistake, and
    /// an exception inside a binding loop would take the rest of the View's bindings with it.
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
                Debug.LogError($"[{nameof(FormatStringMonoBinder)}] Format '{_format}' does not match {values.Length} inputs: {exception.Message}", this);
                return string.Empty;
            }
        }
    }
}
