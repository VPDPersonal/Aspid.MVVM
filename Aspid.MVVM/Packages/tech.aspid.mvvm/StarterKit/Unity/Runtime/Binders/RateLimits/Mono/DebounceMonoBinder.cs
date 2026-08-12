// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="RateLimitedMonoBinder{TValue}"/> that forwards a value only once the values stop coming.
    /// </summary>
    /// <remarks>
    /// The search field case: a value per keystroke becomes one value once the user pauses, which is the difference
    /// between a request per character and a request per word.
    /// <para/>
    /// Every new value restarts the wait, so a fast typist produces exactly one forwarded value. Nothing is forwarded if
    /// the binding is released while a value is still waiting — an answer to a query nobody is looking at any more is
    /// worse than no answer.
    /// </remarks>
    /// <typeparam name="TValue">The type of value being forwarded.</typeparam>
    public abstract class DebounceMonoBinder<TValue> : RateLimitedMonoBinder<TValue>
    {
        private TValue _pending;
        private float _remaining;
        private bool _hasPending;

        /// <inheritdoc/>
        protected override void OnValue(TValue value)
        {
            _pending = value;
            _hasPending = true;
            _remaining = Seconds;
        }

        /// <inheritdoc/>
        protected override void Tick(float deltaTime)
        {
            if (!_hasPending) return;

            _remaining -= deltaTime;
            if (_remaining > 0f) return;

            var value = _pending;

            _pending = default;
            _hasPending = false;

            Emit(value);
        }

        /// <inheritdoc/>
        protected override void Reset()
        {
            _pending = default;
            _hasPending = false;
            _remaining = 0f;
        }
    }
}
