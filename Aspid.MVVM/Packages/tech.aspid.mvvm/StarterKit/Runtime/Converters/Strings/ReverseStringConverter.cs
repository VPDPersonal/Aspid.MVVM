#nullable enable
using System;
using System.Text;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Writes a string back to front.
    /// </summary>
    /// <remarks>Surrogate pairs keep their order; combining marks are not regrouped.</remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/String",
        Name = "Reverse",
        Tooltip = "Writes a string back to front")]
    public sealed class ReverseStringConverter : IConverter<string?, string?>
    {
        [NonSerialized] private StringBuilder? _builder;

        /// <summary>
        /// Reverses the specified string.
        /// </summary>
        /// <param name="value">The string to reverse.</param>
        /// <returns>The string, back to front.</returns>
        public string? Convert(string? value)
        {
            if (string.IsNullOrWhiteSpace(value)) return value;

            _builder ??= new StringBuilder();
            _builder.Clear();

            for (var i = value.Length - 1; i >= 0; i--)
            {
                if (i > 0 && char.IsLowSurrogate(value[i]) && char.IsHighSurrogate(value[i - 1]))
                {
                    _builder.Append(value[i - 1]).Append(value[i]);
                    i--;
                    continue;
                }

                _builder.Append(value[i]);
            }

            return _builder.ToString();
        }
    }
}
