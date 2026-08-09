#if ASPID_MVVM_UNITY_LOCALIZATION_INTEGRATION
#nullable enable
using System;
using UnityEngine;
using UnityEngine.Localization;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Looks a key up in a localization table.
    /// </summary>
    /// <remarks>
    /// The package has localized binders, but they are a separate path — a localized label cannot
    /// also be truncated, recased or wrapped in rich text, because those are converters and the
    /// localized binder has no converter slot. As a converter, localization joins the same chain as
    /// everything else.
    /// <para>
    /// Only compiled when <c>com.unity.localization</c> is installed.
    /// </para>
    /// </remarks>
    [Serializable]
    public sealed class LocalizedStringConverter : IConverterString
    {
        [Tooltip("The string table the key is looked up in.")]
        [SerializeField] private LocalizedStringTable _table = new();

        [Tooltip("Show the key itself when it has no entry, rather than the format below.")]
        [SerializeField] private bool _fallbackToKey = true;

        [Tooltip("A composite format for a missing entry: {0} is the key.")]
        [SerializeField] private string _missingFormat = "#{0}#";

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizedStringConverter"/> class.
        /// </summary>
        public LocalizedStringConverter() { }

        /// <summary>
        /// Looks the specified key up.
        /// </summary>
        /// <param name="value">The key to look up.</param>
        /// <returns>
        /// The localized text, the key itself, or the missing format — whichever the settings call
        /// for. A blank key comes back unchanged.
        /// </returns>
        public string? Convert(string? value)
        {
            if (string.IsNullOrEmpty(value)) return value;

            var table = _table.GetTable();
            var entry = table?.GetEntry(value);

            if (entry is not null) return entry.GetLocalizedString();

            return _fallbackToKey || string.IsNullOrEmpty(_missingFormat)
                ? value
                : string.Format(_missingFormat, value);
        }
    }
}
#endif
