using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// A composite reverse-binder interface that can propagate vector View values back to the ViewModel
    /// as <see cref="Vector2"/> or <see cref="Vector3"/>.
    /// </summary>
    /// <remarks>
    /// Typically implemented alongside <see cref="IVectorBinder"/> on vector UI binders.
    /// </remarks>
    // ReSharper disable once PossibleInterfaceMemberAmbiguity
    public interface IVectorReverseBinder : IReverseBinder<Vector2>, IReverseBinder<Vector3>
    {
        /// <summary>
        /// Raised when the View value changes and should be propagated to a <see cref="Vector2"/> binding target.
        /// </summary>
        public event Action<Vector2> Vector2ValueChanged;

        /// <summary>
        /// Raised when the View value changes and should be propagated to a <see cref="Vector3"/> binding target.
        /// </summary>
        public event Action<Vector3> Vector3ValueChanged;

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