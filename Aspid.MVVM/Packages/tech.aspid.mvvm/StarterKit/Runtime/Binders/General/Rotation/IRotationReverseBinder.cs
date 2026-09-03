using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="IVectorReverseBinder"/> that also reports a <see cref="Quaternion"/>.
    /// </summary>
    public interface IRotationReverseBinder : IVectorReverseBinder, IReverseBinder<Quaternion>
    {
        /// <summary>
        /// Raised with the new rotation to send to a <see cref="Quaternion"/> member of the ViewModel.
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
