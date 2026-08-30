using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="MonoBinder"/> that feeds one value into an
    /// <see cref="AggregatorMonoBinder{TInput, TResult}"/>.
    /// </summary>
    /// <typeparam name="TInput">The type of value this input contributes.</typeparam>
    /// <typeparam name="TResult">The type of the aggregator's combined value.</typeparam>
    public abstract partial class AggregatorInputMonoBinder<TInput, TResult> : MonoBinder, IBinder<TInput>
    {
        [Tooltip("The aggregator this input writes into. Required — inert without it.")]
        [SerializeField] private AggregatorMonoBinder<TInput, TResult> _aggregator;

        [Tooltip("Index of this input inside the aggregator. Each input needs its own.")]
        [SerializeField] [Min(0)] private int _index;

        /// <summary>
        /// Writes <paramref name="value"/> into the aggregator under this input's index.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        /// <remarks>Logs an error and does nothing when no aggregator is assigned.</remarks>
        [BinderLog]
        public void SetValue(TInput value)
        {
            if (!_aggregator)
            {
                this.LogError("no aggregator is assigned", "The value is dropped.");
                return;
            }

            _aggregator.SetInput(_index, value);
        }

        /// <summary>
        /// Called when the binder is unbound. Clears this input's own slot in the aggregator.
        /// </summary>
        protected override void OnUnbound()
        {
            if (_aggregator) _aggregator.ClearInput(_index);
        }
    }
}
