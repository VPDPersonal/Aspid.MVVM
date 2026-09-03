using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Reverse binder that reports a vector to both <see cref="Vector2"/> and <see cref="Vector3"/> members of the ViewModel.
    /// </summary>
    // ReSharper disable once PossibleInterfaceMemberAmbiguity
    public interface IVectorReverseBinder : IReverseBinder<Vector2>, IReverseBinder<Vector3>
    {
        /// <summary>
        /// Raised with the new value to send to a <see cref="Vector2"/> member of the ViewModel.
        /// </summary>
        event Action<Vector2> Vector2ValueChanged;

        /// <summary>
        /// Raised with the new value to send to a <see cref="Vector3"/> member of the ViewModel.
        /// </summary>
        event Action<Vector3> Vector3ValueChanged;

        /// <inheritdoc/>
        event Action<Vector2> IReverseBinder<Vector2>.ValueChanged
        {
            add => Vector2ValueChanged += value;
            remove => Vector2ValueChanged -= value;
        }

        /// <inheritdoc/>
        event Action<Vector3> IReverseBinder<Vector3>.ValueChanged
        {
            add => Vector3ValueChanged += value;
            remove => Vector3ValueChanged -= value;
        }
    }
}
