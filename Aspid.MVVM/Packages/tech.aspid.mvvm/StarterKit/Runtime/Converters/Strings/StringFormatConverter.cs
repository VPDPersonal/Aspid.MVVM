using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Converts string values by applying a format string with optional handling of empty values.
    /// </summary>
    [Serializable]
    public class StringFormatConverter : GenericToString<string>, IConverterString
    {
        [SerializeField] private bool _formatEmptyValues;

        public StringFormatConverter() { }

        /// <param name="format">The format string to apply using <see cref="string.Format(string, object)"/>.</param>
        /// <param name="formatEmptyValues">If <c>true</c>, applies the format even when the input value is empty or whitespace-only. Default is <c>false</c>.</param>
        public StringFormatConverter(string format, bool formatEmptyValues = false)
            : base(format)
        {
            _formatEmptyValues = formatEmptyValues;
        }

        protected override string Format(string value) => _formatEmptyValues || !string.IsNullOrWhiteSpace(value) 
            ? base.Format(value)
            : value;
    }
}