// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract <see cref="RateLimitedMonoBinder{TValue}"/> that forwards at most one value per interval.
    /// </summary>
    /// <remarks>
    /// The first value of an interval is forwarded at once; the latest value that arrives during the interval is
    /// forwarded when it ends.
    /// </remarks>
    /// <typeparam name="TValue">The type of the forwarded value.</typeparam>
    public abstract class ThrottleMonoBinder<TValue> : RateLimitedMonoBinder<TValue>
    {
        private TValue _pending;
        private float _remaining;
        private bool _hasPending;

        /// <inheritdoc/>
        protected override void OnValue(TValue value)
        {
            if (_remaining > 0f)
            {
                _pending = value;
                _hasPending = true;
                return;
            }

            _remaining = Seconds;
            Emit(value);
        }

        /// <inheritdoc/>
        protected override void Tick(float deltaTime)
        {
            if (_remaining <= 0f) return;

            _remaining -= deltaTime;
            if (_remaining > 0f || !_hasPending) return;

            var value = _pending;
            _pending = default;
            _hasPending = false;
            _remaining = Seconds;

            Emit(value);
        }

        /// <inheritdoc/>
        protected override void Clear()
        {
            _pending = default;
            _hasPending = false;
            _remaining = 0f;
        }
    }
}
