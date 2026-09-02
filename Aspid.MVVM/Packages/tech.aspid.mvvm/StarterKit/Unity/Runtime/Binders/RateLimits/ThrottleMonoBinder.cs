// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="RateLimitedMonoBinder{TValue}"/> that forwards at most one value per interval.
    /// </summary>
    /// <remarks>
    /// The first value in each interval is forwarded immediately. A value that arrives before the interval elapses
    /// replaces any already held and is the one emitted when it ends.
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
