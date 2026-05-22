using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Converts object references to boolean based on null check, with optional inversion.
    /// </summary>
    [Serializable]
    public class ObjectNullToBoolConverter : IConverterObjectToBool
    {
#if UNITY_2022_1_OR_NEWER
        [UnityEngine.SerializeField]
#endif
        private bool _isInvert;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectNullToBoolConverter"/> class.
        /// </summary>
        public ObjectNullToBoolConverter()
            : this(isInvert: false) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectNullToBoolConverter"/> class.
        /// </summary>
        /// <param name="isInvert">If <c>true</c>, inverts the result of the null check. Default is <c>false</c>.</param>
        public ObjectNullToBoolConverter(bool isInvert)
        {
            _isInvert = isInvert;
        }

        /// <summary>
        /// Converts an object to boolean based on whether it is null.
        /// </summary>
        /// <param name="value">The object to check.</param>
        /// <returns><c>true</c> if the value is null (or not null if inverted), otherwise <c>false</c>.</returns>
        public bool Convert(object? value)
        {
            var isNull = value is null;
            return _isInvert ? !isNull : isNull;
        }
    }
}