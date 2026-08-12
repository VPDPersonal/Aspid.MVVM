// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="RateLimitedMonoBinder{TValue}"/> that forwards at most one value per interval.
    /// </summary>
    /// <remarks>
    /// The opposite end from a debounce: a source that publishes every frame — a position, a timer, a physics value —
    /// reaches the view often enough to look live and rarely enough to be affordable.
    /// <para/>
    /// The first value goes through immediately, because waiting out the interval before showing anything makes the view
    /// look broken. A value that arrives inside the interval is held, and the last one held is what goes out when the
    /// interval ends — an intermediate value nobody saw is not worth delaying the current one for.
    /// </remarks>
    /// <typeparam name="TValue">The type of value being forwarded.</typeparam>
    public abstract class ThrottleMonoBinder<TValue> : RateLimitedMonoBinder<TValue>
    {
        private TValue _pending;
        private float _remaining;
        private bool _hasPending;

        /// <inheritdoc/>
        protected override void OnValue(TValue value)
        {
            if (_remaining <= 0f)
            {
                _remaining = Seconds;
                Emit(value);

                return;
            }

            _pending = value;
            _hasPending = true;
        }

        /// <inheritdoc/>
        protected override void Tick(float deltaTime)
        {
            if (_remaining <= 0f) return;

            _remaining -= deltaTime;
            if (_remaining > 0f) return;

            if (!_hasPending) return;

            var value = _pending;

            _pending = default;
            _hasPending = false;
            _remaining = Seconds;

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
