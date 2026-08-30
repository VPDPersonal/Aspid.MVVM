using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// A composite reverse-binder interface that can propagate numeric View values back to the ViewModel
    /// as <see cref="int"/>, <see cref="long"/>, <see cref="float"/>, or <see cref="double"/>.
    /// </summary>
    /// <remarks>
    /// Implementors provide a <see cref="NumberReverseChannel"/> and nothing else: the
    /// <see cref="IReverseBinder{T}.ValueChanged"/> of all four base interfaces is bridged to that channel
    /// here. One <c>Raise</c> on the channel reaches every numeric type.
    /// </remarks>
    // ReSharper disable once PossibleInterfaceMemberAmbiguity
    public interface INumberReverseBinder : IReverseBinder<int>, IReverseBinder<long>, IReverseBinder<float>, IReverseBinder<double>
    {
        /// <summary>
        /// Gets the channel holding this binder's numeric subscriptions.
        /// </summary>
        /// <remarks>
        /// Returned by reference: the channel is a mutable field of the implementor, and a copy would
        /// collect subscriptions the binder never raises.
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
