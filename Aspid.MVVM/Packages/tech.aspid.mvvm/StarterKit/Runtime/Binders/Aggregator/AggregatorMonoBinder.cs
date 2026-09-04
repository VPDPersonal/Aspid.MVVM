using UnityEngine;
using UnityEngine.Events;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract <see cref="MonoBehaviour"/> that combines the values of several
    /// <see cref="AggregatorInputMonoBinder{TInput, TResult}"/> components into one <see cref="UnityEvent{T}"/>.
    /// </summary>
    /// <remarks>
    /// Nothing is forwarded until every input has reported at least once.
    /// </remarks>
    /// <typeparam name="TInput">The type each input contributes.</typeparam>
    /// <typeparam name="TResult">The type of the combined value.</typeparam>
    public abstract class AggregatorMonoBinder<TInput, TResult> : MonoBehaviour
    {
        [Tooltip("Number of inputs; nothing forwards until all have reported.")]
        [SerializeField] [Min(1)] private int _inputCount = 2;

        [Tooltip("Invoked with the combined value.")]
        [SerializeField] private UnityEvent<TResult> _result;

        private TInput[] _values;
        private bool[] _reported;

        /// <summary>
        /// Gets the number of inputs the aggregator expects.
        /// </summary>
        public int InputCount => _inputCount;

        /// <summary>
        /// Stores one input's value and forwards the combined result once every input has reported.
        /// </summary>
        /// <param name="index">The input index.</param>
        /// <param name="value">The value that input received.</param>
        public void SetInput(int index, TInput value)
        {
            EnsureSize();

            if (index < 0 || index >= _values.Length)
            {
                BinderLogger.LogError(
                    GetType(),
                    problem: $"the input index {index} is outside the configured count of {InputCount}",
                    consequence: "The value is dropped.",
                    context: this);

                return;
            }

            _values[index] = value;
            _reported[index] = true;

            foreach (var reported in _reported)
            {
                if (!reported) return;
            }

            _result?.Invoke(Combine(_values));
        }

        /// <summary>
        /// Forgets one input's value, so it has to report again before the next combine.
        /// </summary>
        /// <param name="index">The input index.</param>
        public void ClearInput(int index)
        {
            if (_reported is null || index < 0 || index >= _reported.Length) return;

            _reported[index] = false;
            _values[index] = default;
        }

        /// <summary>
        /// Combines the values every input has reported.
        /// </summary>
        /// <param name="values">One value per input, in input order.</param>
        /// <returns>The value to forward.</returns>
        protected abstract TResult Combine(TInput[] values);

        private void EnsureSize()
        {
            if (_values is not null && _values.Length == InputCount) return;

            _values = new TInput[InputCount];
            _reported = new bool[InputCount];
        }
    }
}
