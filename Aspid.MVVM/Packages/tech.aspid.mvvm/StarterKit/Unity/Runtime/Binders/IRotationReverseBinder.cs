using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Extends <see cref="IVectorReverseBinder"/> with <see cref="Quaternion"/> reverse binding support.
    /// </summary>
    public interface IRotationReverseBinder : IVectorReverseBinder, IReverseBinder<Quaternion>
    {
        /// <summary>
        /// Raised when the bound <see cref="Quaternion"/> rotation value changes.
        /// </summary>
        event Action<Quaternion> RotationValueChanged;

        /// <inheritdoc/>
        event Action<Quaternion> IReverseBinder<Quaternion>.ValueChanged
        {
            add => RotationValueChanged += value;
            remove => RotationValueChanged -= value;
        }
    }
}