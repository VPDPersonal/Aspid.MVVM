using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="MonoBinder"/> that feeds one value into an
    /// <see cref="AggregatorMonoBinder{TInput, TResult}"/>.
    /// </summary>
    /// <remarks>
    /// One of these per source: each is an ordinary binder bound to its own member, and each writes into the same
    /// aggregator under its own index. That keeps the framework's rule - one binder, one member - while letting an answer
    /// depend on several of them.
    /// <para/>
    /// Unbinding forgets what the aggregator had collected, so a pooled object does not combine one row's values with
    /// another's.
    /// </remarks>
    /// <typeparam name="TInput">The type of value this input contributes.</typeparam>
    /// <typeparam name="TResult">The type of the aggregator's combined value.</typeparam>
    public abstract partial class AggregatorInputMonoBinder<TInput, TResult> : MonoBinder, IBinder<TInput>
    {
        [Tooltip("The aggregator this input writes into. Required — without it the input is inert and the aggregator never completes.")]
        [SerializeField] private AggregatorMonoBinder<TInput, TResult> _aggregator;

        [Tooltip("Index of this input inside the aggregator. Every input on the same aggregator needs its own.")]
        [SerializeField] [Min(0)] private int _index;

        /// <summary>
        /// Writes <paramref name="value"/> into the aggregator under this input's index.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        /// <remarks>
        /// Logs an error and writes nothing when no aggregator is assigned. The alternative is worse than a missing
        /// value: the aggregator waits for an input that never reports, and forwards nothing at all.
        /// </remarks>
        [BinderLog]
        public void SetValue(TInput value)
        {
            if (!_aggregator)
            {
                Debug.LogError($"[{GetType().Name}] No aggregator assigned.", context: this);
                return;
            }

            _aggregator.SetInput(_index, value);
        }

        /// <summary>
        /// Called when the binder is unbound. Clears the aggregator's collected values.
        /// </summary>
        protected override void OnUnbound()
        {
            if (_aggregator) _aggregator.ResetInputs();
        }
    }
}
