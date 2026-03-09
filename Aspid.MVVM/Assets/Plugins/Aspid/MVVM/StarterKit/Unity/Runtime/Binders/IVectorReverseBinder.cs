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
    /// Exposes two strongly typed events — <see cref="Vector2ValueChanged"/> and <see cref="Vector3ValueChanged"/> — that implementors raise when
    /// the View value changes. Default interface method implementations bridge these concrete events to the
    /// generic <see cref="IReverseBinder{T}.ValueChanged"/> event required by each <c>IReverseBinder</c>
    /// base interface, so the binding infrastructure can subscribe via a single, type-safe surface.
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