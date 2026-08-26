using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Removes surrounding characters from a string.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/String",
        Name = "Trim",
        Tooltip = "Removes surrounding characters from a string")]
    public sealed class TrimStringConverter : IConverter<string?, string?>, ISerializationCallbackReceiver
    {
        [Tooltip("Which ends to trim.")]
        [SerializeField] private TrimSide _side = TrimSide.Both;

        [Tooltip("The characters to remove. When empty, whitespace is removed.")]
        [SerializeField] private string _trimChars = string.Empty;

        [NonSerialized] private char[]? _trimCharsCache;

        /// <remarks>Default: trimming whitespace from both ends.</remarks>
        public TrimStringConverter() { }

        /// <param name="side">Which ends to trim.</param>
        /// <param name="trimChars">The characters to remove. When empty, whitespace is removed.</param>
        public TrimStringConverter(TrimSide side, string trimChars = "")
        {
            _side = side;
            _trimChars = trimChars;
        }

        /// <summary>
        /// Trims the specified string.
        /// </summary>
        /// <param name="value">The string to trim.</param>
        /// <returns>
        /// The trimmed string — or the string unchanged when the side is not a declared value.
        /// </returns>
        public string? Convert(string? value)
        {
            if (value is null) return null;

            var chars = TrimChars();

            return _side switch
            {
                TrimSide.Both => chars is null ? value.Trim() : value.Trim(chars),
                TrimSide.Start => chars is null ? value.TrimStart() : value.TrimStart(chars),
                TrimSide.End => chars is null ? value.TrimEnd() : value.TrimEnd(chars),
                _ => Undeclared(value)
            };
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize() { }

        // TODO Aspid.MVVM – Verify in the Editor that Unity calls OnAfterDeserialize on a converter held
        // in a [SerializeReference] field after an Inspector edit, not only on load. Five converters now
        // drop their cache this way instead of keeping a copy of every setting to compare against:
        // TrimStringConverter, ThousandsSeparatorConverter, StringToVector2Converter,
        // StringToVector3Converter and EnumFlagsToStringConverter. If it is not called, they need the
        // host binder to invalidate them from OnValidate instead.

        // The one moment the authored field changes: Unity reads the object again after every edit.
        void ISerializationCallbackReceiver.OnAfterDeserialize() =>
            _trimCharsCache = null;

        private string Undeclared(string value)
        {
            this.LogError($"the side {_side.Describe()} is not a declared {nameof(TrimSide)}",
                "Returning the value unchanged.");

            return value;
        }

        // ToCharArray allocates and a binder pushes on every notification, so the array is made once.
        // An empty one stands for "trim whitespace", which is what Trim does with no argument.
        private char[]? TrimChars()
        {
            _trimCharsCache ??= string.IsNullOrEmpty(_trimChars)
                ? Array.Empty<char>()
                : _trimChars.ToCharArray();

            return _trimCharsCache.Length is 0 ? null : _trimCharsCache;
        }
    }
}
