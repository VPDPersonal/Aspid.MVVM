using System;
using UnityEngine;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Generic converter that transforms values to strings with optional formatting.
    /// </summary>
    /// <typeparam name="TFrom">The type of the value to convert.</typeparam>
    [Serializable]
    public class GenericToString<TFrom> : IConverter<TFrom?, string?>
    {
        [Tooltip("Optional format string applied to the value. Leave empty for the type's default formatting.")]
        [SerializeField] private string? _format;
        
        public GenericToString()
        {
            _format = string.Empty;
        }

        /// <param name="format">The format string to apply using <see cref="string.Format(string, object)"/>.</param>
        public GenericToString(string format)
        {
            _format = format;
        }

        /// <summary>
        /// Converts the specified value to a string using the configured format.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The string representation of the value, or <c>null</c> if the value is <c>null</c>.</returns>
        public string? Convert(TFrom? value)
        {
            if (value is null) return null;
            if (string.IsNullOrWhiteSpace(_format)) return value.ToString();
            
            return Format(value);
        }
        
        protected virtual string Format(TFrom value) => 
            string.Format(_format, value);
    }
}