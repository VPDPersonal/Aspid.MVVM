using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Composite <see cref="IReverseBinder{T}"/> that reports a numeric View value to the ViewModel as
    /// <see cref="int"/>, <see cref="long"/>, <see cref="float"/> or <see cref="double"/>.
    /// Implementors provide only <see cref="Channel"/>; the four events are bridged to it here.
    /// </summary>
    // ReSharper disable once PossibleInterfaceMemberAmbiguity
    public interface INumberReverseBinder : IReverseBinder<int>, IReverseBinder<long>, IReverseBinder<float>, IReverseBinder<double>
    {
        /// <summary>
        /// Gets the channel holding this binder's numeric subscriptions.
        /// </summary>
        /// <remarks>
        /// Must return a reference to a field: a copy would collect subscriptions the binder never raises.
        /// </remarks>
        protected ref NumberReverseChannel Channel { get; }

        /// <inheritdoc/>
        event Action<int>? IReverseBinder<int>.ValueChanged
        {
            add => Channel.IntValueChanged += value;
            remove => Channel.IntValueChanged -= value;
        }

        /// <inheritdoc/>
        event Action<long>? IReverseBinder<long>.ValueChanged
        {
            add => Channel.LongValueChanged += value;
            remove => Channel.LongValueChanged -= value;
        }

        /// <inheritdoc/>
        event Action<float>? IReverseBinder<float>.ValueChanged
        {
            add => Channel.FloatValueChanged += value;
            remove => Channel.FloatValueChanged -= value;
        }

        /// <inheritdoc/>
        event Action<double>? IReverseBinder<double>.ValueChanged
        {
            add => Channel.DoubleValueChanged += value;
            remove => Channel.DoubleValueChanged -= value;
        }
    }
}
