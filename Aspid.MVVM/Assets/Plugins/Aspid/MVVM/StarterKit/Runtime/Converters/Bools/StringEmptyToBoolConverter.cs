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
#if UNITY_2022_1_OR_NEWER
        [UnityEngine.SerializeField]
#endif
        private bool _isInvert;

        /// <summary>
        /// Initializes a new instance of the <see cref="StringEmptyToBoolConverter"/> class.
        /// </summary>
        public StringEmptyToBoolConverter()
            : this(isInvert: false) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringEmptyToBoolConverter"/> class.
        /// </summary>
        /// <param name="isInvert">If <c>true</c>, inverts the result of the empty check. Default is <c>false</c>.</param>
        public StringEmptyToBoolConverter(bool isInvert)
        {
            _isInvert = isInvert;
        }

        /// <summary>
        /// Converts a string to boolean based on whether it is null or empty.
        /// </summary>
        /// <param name="value">The string to check.</param>
        /// <returns><c>true</c> if the value is null or empty (or not if inverted), otherwise <c>false</c>.</returns>
        public bool Convert(string? value)
        {
            var isEmpty = string.IsNullOrEmpty(value);
            return _isInvert ? !isEmpty : isEmpty;
        }
    }
}