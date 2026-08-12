using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Converts string values to boolean based on empty check, with optional inversion.
    /// </summary>
    [Serializable]
    public class StringEmptyToBoolConverter : IConverterStringToBool
    {
        [UnityEngine.SerializeField]
        private bool _isInvert;

        /// <summary>
        /// Initializes a new instance of the <see cref="StringEmptyToBoolConverter"/> class.
        /// </summary>
        public StringEmptyToBoolConverter()
            : this(isInvert: false) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringEmptyToBoolConverter"/> class.
        /// </summary>
        /// <param name="isInvert">If <see langword="true"/>, inverts the result of the empty check. Default is <see langword="false"/>.</param>
        public StringEmptyToBoolConverter(bool isInvert)
        {
            _isInvert = isInvert;
        }

        /// <summary>
        /// Converts a string to boolean based on whether it is null or empty.
        /// </summary>
        /// <param name="value">The string to check.</param>
        /// <returns><see langword="true"/> if the value is null or empty (or not if inverted), otherwise <see langword="false"/>.</returns>
        public bool Convert(string? value)
        {
            var isEmpty = string.IsNullOrEmpty(value);
            return _isInvert ? !isEmpty : isEmpty;
        }
    }
}