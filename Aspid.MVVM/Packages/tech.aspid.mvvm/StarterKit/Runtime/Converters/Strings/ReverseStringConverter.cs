using Aspid.FastTools.Types;
using System;
using System.Text;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Writes a string back to front.
    /// </summary>
    /// <remarks>
    /// A puzzle, a mirrored effect, a name shown in reverse for a reveal. Surrogate pairs — emoji and
    /// everything else outside the basic plane — keep their own order, because a reversed pair is not
    /// a character at all. Combining marks are not regrouped: an accent written as its own code point
    /// lands on the letter that used to precede it.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/String", Name = "Reverse String", Tooltip = "Writes a string back to front")]
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
            if (string.IsNullOrEmpty(value)) return value;

            _builder ??= new StringBuilder();
            _builder.Clear();

            for (var i = value!.Length - 1; i >= 0; i--)
            {
                // A surrogate pair is one character stored as two, and it only means anything in the
                // order it was written.
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
