#nullable enable
using System;
using UnityEngine;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Converts a <see cref="Object"/> reference to a boolean based on whether it is alive.
    /// </summary>
    /// <remarks>
    /// <see cref="ObjectNullToBoolConverter"/> asks <c>value is null</c>, which reports
    /// <see langword="false"/> for a destroyed object — its managed reference outlives the native
    /// one. A crosshair bound to a destroyed target would stay on screen. This uses Unity's
    /// overloaded <c>==</c>, which is the only check that catches both cases.
    /// </remarks>
    [Serializable]
    public sealed class UnityObjectNullToBoolConverter : IConverter<Object?, bool>
    {
        [Tooltip("Invert the result — true when the object is alive.")]
        [SerializeField] private bool _isInvert;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnityObjectNullToBoolConverter"/> class.
        /// </summary>
        public UnityObjectNullToBoolConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnityObjectNullToBoolConverter"/> class.
        /// </summary>
        /// <param name="isInvert">If <see langword="true"/>, inverts the result.</param>
        public UnityObjectNullToBoolConverter(bool isInvert)
        {
            _isInvert = isInvert;
        }

        /// <summary>
        /// Tests whether the specified object is missing or destroyed.
        /// </summary>
        /// <param name="value">The object to test.</param>
        /// <returns>
        /// <see langword="true"/> when the object is unassigned or destroyed, inverted when configured.
        /// </returns>
        public bool Convert(Object? value)
        {
            // Deliberately Unity's overloaded ==: `is null` misses a destroyed object.
            var isMissing = value == null;
            return _isInvert ? !isMissing : isMissing;
        }
    }
}
