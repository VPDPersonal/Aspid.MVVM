using UnityEngine;
using UnityEngine.Events;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="MonoBehaviour"/> that combines several bound values into one and forwards the result to a
    /// target <see cref="UnityEvent{T}"/>.
    /// </summary>
    /// <remarks>
    /// Not a binder itself — it is the shared point that several <see cref="AggregatorInputMonoBinder{TInput, TResult}"/>
    /// components write into. Nothing is forwarded until every input has reported at least once.
    /// </remarks>
    /// <typeparam name="TInput">The type of value each input contributes.</typeparam>
    /// <typeparam name="TResult">The type of the combined value.</typeparam>
    public abstract class AggregatorMonoBinder<TInput, TResult> : MonoBehaviour
    {
        [Tooltip("How many inputs are expected. Nothing forwards until all have reported.")]
        [SerializeField] [Min(1)] private int _inputCount = 2;

        [Tooltip("Invoked with the combined value on every change, once all have reported.")]
        [SerializeField] private UnityEvent<TResult> _result;

        private TInput[] _values;
        private bool[] _reported;
        private Component[] _inputs;

        /// <summary>
        /// Gets how many inputs the aggregator expects.
        /// </summary>
        public int InputCount => Mathf.Max(1, _inputCount);

        /// <summary>
        /// Claims an input index for <paramref name="input"/>.
        /// </summary>
        /// <param name="index">Index of the input, as configured on the input binder.</param>
        /// <param name="input">The input binder claiming the index.</param>
        /// <remarks>
        /// Logs an error when the index is outside the configured count or already claimed by another input — a
        /// duplicated index leaves another one without a source, so the aggregator would otherwise stay silent forever.
        /// </remarks>
        public void RegisterInput(int index, Component input)
        {
            EnsureSize();

            if (index < 0 || index >= _inputs.Length)
            {
                BinderLogger.LogError(GetType(), $"the input index {index} is outside the configured count of {InputCount}", "The input is ignored.", this);
                return;
            }

            if (_inputs[index] && _inputs[index] != input)
            {
                BinderLogger.LogError(GetType(), $"the input index {index} is already used by {_inputs[index].name}", "Another index stays without a source, so nothing is forwarded.", this);
                return;
            }

            _inputs[index] = input;
        }

        /// <summary>
        /// Releases the input index claimed by <paramref name="input"/>.
        /// </summary>
        /// <param name="index">Index of the input, as configured on the input binder.</param>
        /// <param name="input">The input binder releasing the index.</param>
        public void UnregisterInput(int index, Component input)
        {
            if (_inputs is null || index < 0 || index >= _inputs.Length) return;
            if (_inputs[index] == input) _inputs[index] = null;
        }

        /// <summary>
        /// Stores one input's value and forwards the combined result once every input has reported.
        /// </summary>
        /// <param name="index">Index of the input, as configured on the input binder.</param>
        /// <param name="value">The value that input received from the ViewModel.</param>
        /// <remarks>Logs an error and does nothing when <paramref name="index"/> is outside the configured count.</remarks>
        public void SetInput(int index, TInput value)
        {
            EnsureSize();

            if (index < 0 || index >= _values.Length)
            {
                BinderLogger.LogError(GetType(), $"the input index {index} is outside the configured count of {InputCount}", "The value is dropped.", this);
                return;
            }

            _values[index] = value;
            _reported[index] = true;

            for (var i = 0; i < _reported.Length; i++)
            {
                if (!_reported[i]) return;
            }

            _result?.Invoke(Combine(_values));
        }

        /// <summary>
        /// Forgets which inputs have reported, so a reused object does not combine values from a previous binding.
        /// </summary>
        public void ResetInputs()
        {
            if (_reported is null) return;

            for (var i = 0; i < _reported.Length; i++)
            {
                _reported[i] = false;
                _values[i] = default;
            }
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
            _inputs = new Component[InputCount];
        }
    }
}
