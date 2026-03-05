using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Converts string values by applying a format string with optional handling of empty values.
    /// </summary>
    [Serializable]
    public class StringFormatConverter : IConverterString
    {
#if UNITY_2022_1_OR_NEWER
        [UnityEngine.SerializeField]
#endif
        private string _format;
#if UNITY_2022_1_OR_NEWER
        [UnityEngine.SerializeField]
#endif
        private bool _formatEmptyValues;

        /// <summary>
        /// Initializes a new instance of the <see cref="StringFormatConverter"/> class with default settings.
        /// </summary>
        public StringFormatConverter()
        {
            _format = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringFormatConverter"/> class.
        /// </summary>
        /// <param name="format">The format string to apply using <see cref="string.Format(string, object)"/>.</param>
        /// <param name="formatEmptyValues">If <c>true</c>, applies the format even when the input value is empty or whitespace-only. Default is <c>false</c>.</param>
        public StringFormatConverter(string format, bool formatEmptyValues = false)
        {
            _format = format;
            _formatEmptyValues = formatEmptyValues;
        }

        /// <summary>
        /// Converts the specified string value by applying the configured format string.
        /// </summary>
        /// <param name="value">The input string value to format.</param>
        /// <returns>The formatted string, or the original value if the format string is empty or conditions are not met.</returns>
        public string? Convert(string? value)
        {
            if (string.IsNullOrWhiteSpace(_format)) return value;

            return _formatEmptyValues || !string.IsNullOrWhiteSpace(value)
                ? string.Format(_format, value)
                : value;
        }
    }
}