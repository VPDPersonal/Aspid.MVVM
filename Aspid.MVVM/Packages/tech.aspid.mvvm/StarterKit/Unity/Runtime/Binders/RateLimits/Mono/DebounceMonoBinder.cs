// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="RateLimitedMonoBinder{TValue}"/> that forwards a value only once the values stop coming.
    /// </summary>
    /// <remarks>A value pending when the binding is released is dropped, not forwarded.</remarks>
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
