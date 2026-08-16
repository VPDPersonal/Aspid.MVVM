#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Applies a fixed rotation on top of a bound one.
    /// </summary>
    /// <remarks>
    /// Correcting for a model that was authored facing the wrong way, without the ViewModel knowing
    /// which way the art faces.
    /// </remarks>
    [Serializable]
    public sealed class QuaternionOffsetConverter : ITwoWayConverter<Quaternion, Quaternion>
    {
        [Tooltip("The rotation applied on top of the bound one, in Euler degrees.")]
        [SerializeField] private Vector3 _offsetEuler;

        [Tooltip("Apply the offset before the bound rotation rather than after.")]
        [SerializeField] private bool _applyFirst;

        public QuaternionOffsetConverter() { }

        /// <param name="offsetEuler">The rotation applied on top of the bound one, in Euler degrees.</param>
        /// <param name="applyFirst">Whether to apply the offset before the bound rotation.</param>
        public QuaternionOffsetConverter(Vector3 offsetEuler, bool applyFirst = false)
        {
            _offsetEuler = offsetEuler;
            _applyFirst = applyFirst;
        }

        /// <summary>
        /// Applies the offset to the specified rotation.
        /// </summary>
        /// <param name="value">The rotation to adjust.</param>
        /// <returns>The adjusted rotation.</returns>
        public Quaternion Convert(Quaternion value)
        {
            var offset = Quaternion.Euler(_offsetEuler);
            return _applyFirst ? offset * value : value * offset;
        }

        /// <summary>
        /// Removes the offset from the specified rotation.
        /// </summary>
        /// <param name="value">The rotation to adjust.</param>
        /// <returns>The rotation without the offset.</returns>
        public Quaternion ConvertBack(Quaternion value)
        {
            var inverse = Quaternion.Inverse(Quaternion.Euler(_offsetEuler));
            return _applyFirst ? inverse * value : value * inverse;
        }
    }
}
