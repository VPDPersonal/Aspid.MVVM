// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract <see cref="RateLimitedMonoBinder{TValue}"/> that forwards the last value once no new value has
    /// arrived for the interval.
    /// </summary>
    /// <remarks>
    /// A value still pending on unbind is dropped.
    /// </remarks>
    /// <typeparam name="TValue">The type of the forwarded value.</typeparam>
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
            Clear();
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
