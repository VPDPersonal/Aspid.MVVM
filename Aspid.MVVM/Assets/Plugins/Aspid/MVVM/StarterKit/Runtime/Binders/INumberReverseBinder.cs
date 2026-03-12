using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// A composite reverse-binder interface that can propagate numeric View values back to the ViewModel
    /// as <see cref="int"/>, <see cref="long"/>, <see cref="float"/>, or <see cref="double"/>.
    /// </summary>
    /// <remarks>
    /// Exposes four strongly typed events — <see cref="IntValueChanged"/>, <see cref="LongValueChanged"/>,
    /// <see cref="FloatValueChanged"/>, and <see cref="DoubleValueChanged"/> — that implementors raise when
    /// the View value changes. Default interface method implementations bridge these concrete events to the
    /// generic <see cref="IReverseBinder{T}.ValueChanged"/> event required by each <c>IReverseBinder</c>
    /// base interface, so the binding infrastructure can subscribe via a single, type-safe surface.
    /// Typically implemented alongside <see cref="INumberBinder"/> on numeric UI binders such as slider
    /// or input-field binders.
    /// </remarks>
    // ReSharper disable once PossibleInterfaceMemberAmbiguity
    public interface INumberReverseBinder : IReverseBinder<int>, IReverseBinder<long>, IReverseBinder<float>, IReverseBinder<double>
    {
        /// <summary>
        /// Raised when the View value changes and should be propagated to an <see cref="int"/> binding target.
        /// </summary>
        public event Action<int> IntValueChanged;

        /// <summary>
        /// Raised when the View value changes and should be propagated to a <see cref="long"/> binding target.
        /// </summary>
        public event Action<long> LongValueChanged;

        /// <summary>
        /// Raised when the View value changes and should be propagated to a <see cref="float"/> binding target.
        /// </summary>
        public event Action<float> FloatValueChanged;

        /// <summary>
        /// Raised when the View value changes and should be propagated to a <see cref="double"/> binding target.
        /// </summary>
        public event Action<double> DoubleValueChanged;

        /// <inheritdoc/>
        event Action<int>? IReverseBinder<int>.ValueChanged
        {
            add => IntValueChanged += value;
            remove => IntValueChanged -= value;
        }

        /// <inheritdoc/>
        event Action<long>? IReverseBinder<long>.ValueChanged
        {
            add => LongValueChanged += value;
            remove => LongValueChanged -= value;
        }

        /// <inheritdoc/>
        event Action<float>? IReverseBinder<float>.ValueChanged
        {
            add => FloatValueChanged += value;
            remove => FloatValueChanged -= value;
        }

        /// <inheritdoc/>
        event Action<double>? IReverseBinder<double>.ValueChanged
        {
            add => DoubleValueChanged += value;
            remove => DoubleValueChanged -= value;
        }
    }
}