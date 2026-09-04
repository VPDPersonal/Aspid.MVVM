using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract <see cref="MonoBinder"/> that feeds one value into an
    /// <see cref="AggregatorMonoBinder{TInput, TResult}"/>.
    /// </summary>
    /// <typeparam name="TInput">The type this input contributes.</typeparam>
    /// <typeparam name="TResult">The type of the aggregator's combined value.</typeparam>
    public abstract partial class AggregatorInputMonoBinder<TInput, TResult> : MonoBinder, IBinder<TInput>
    {
        [Tooltip("Aggregator this input writes into.")]
        [SerializeField] private AggregatorMonoBinder<TInput, TResult> _aggregator;

        [Tooltip("Index of this input inside the aggregator.")]
        [SerializeField] [Min(0)] private int _index;

        /// <summary>
        /// Writes <paramref name="value"/> into the aggregator under this input's index.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(TInput value)
        {
            if (_aggregator)
            {
                _aggregator.SetInput(_index, value);
                return;
            }

            this.LogError(
                problem: "no aggregator is assigned",
                consequence: "The value is dropped.");
        }

        /// <inheritdoc/>
        protected override void OnUnbound()
        {
            if (_aggregator) _aggregator.ClearInput(_index);
        }
    }
}
