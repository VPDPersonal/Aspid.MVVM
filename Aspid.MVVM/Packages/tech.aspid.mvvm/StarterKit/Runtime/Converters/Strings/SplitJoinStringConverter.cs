#nullable enable
using System;
using System.Text;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Splits a string and joins the parts back together with different text.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/String",
        Name = "Split Join",
        Tooltip = "Splits a string and joins the parts back together with different text")]
    public sealed class SplitJoinStringConverter : IConverter<string?, string?>
    {
        [Tooltip("The text the value is split on. When empty, the value passes through.")]
        [SerializeField] private string _splitOn = ",";

        [Tooltip("Placed between the parts when they are joined back.")]
        [SerializeField] private string _joinWith = ", ";

        [Tooltip("How many parts to make. Zero makes as many as there are; the rest stays in the last part.")]
        [SerializeField] [Min(0)] private int _maxParts;

        [Tooltip("Drop the spaces at either end of every part.")]
        [SerializeField] private bool _trimParts = true;

        [NonSerialized] private StringBuilder? _builder;

        /// <remarks>Default: re-spacing a comma-separated list.</remarks>
        public SplitJoinStringConverter() { }

        /// <param name="splitOn">The text the value is split on. When empty, the value passes through.</param>
        /// <param name="joinWith">Placed between the parts when they are joined back.</param>
        /// <param name="maxParts">How many parts to make. Zero makes as many as there are; the rest stays in the last part.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="maxParts"/> is negative.</exception>
        public SplitJoinStringConverter(
            string splitOn,
            string joinWith,
            int maxParts = 0)
        {
            _splitOn = splitOn;
            _joinWith = joinWith;
            _maxParts = maxParts >= 0 ? maxParts : throw new ArgumentOutOfRangeException(nameof(maxParts));
        }

        /// <summary>
        /// Splits the specified string and joins the parts back.
        /// </summary>
        /// <param name="value">The string to re-split.</param>
        /// <returns>The rejoined string, or the value unchanged when it is blank or there is nothing to split on.</returns>
        public string? Convert(string? value)
        {
            if (string.IsNullOrWhiteSpace(value) || string.IsNullOrEmpty(_splitOn)) return value;

            _builder ??= new StringBuilder();
            _builder.Clear();

            var parts = 0;
            var start = 0;

            while (true)
            {
                var isLast = _maxParts > 0 && parts == _maxParts - 1;
                var next = isLast ? -1 : value.IndexOf(_splitOn, start, StringComparison.Ordinal);
                var end = next < 0 ? value.Length : next;

                if (parts > 0) _builder.Append(_joinWith);
                Append(_builder, value, start, end);
                parts++;

                if (next < 0) break;
                start = next + _splitOn.Length;
            }

            return _builder.ToString();
        }

        private void Append(StringBuilder builder, string value, int start, int end)
        {
            if (_trimParts)
            {
                while (start < end && char.IsWhiteSpace(value[start])) start++;
                while (end > start && char.IsWhiteSpace(value[end - 1])) end--;
            }

            builder.Append(value, start, end - start);
        }
    }
}
