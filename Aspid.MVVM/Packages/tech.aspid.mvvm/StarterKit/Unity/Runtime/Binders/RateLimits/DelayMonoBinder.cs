using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="RateLimitedMonoBinder{TValue}"/> that forwards every value, late.
    /// </summary>
    /// <remarks>
    /// Unlike a debounce or a throttle, no value is dropped while bound — each is forwarded in arrival order.
    /// Releasing the binding clears the queue.
    /// </remarks>
    /// <typeparam name="TValue">The type of value being forwarded.</typeparam>
    public abstract class DelayMonoBinder<TValue> : RateLimitedMonoBinder<TValue>
    {
        private readonly Queue<Pending> _pending = new();

        /// <inheritdoc/>
        protected override void OnValue(TValue value) =>
            _pending.Enqueue(new Pending(value, Seconds));

        /// <inheritdoc/>
        protected override void Tick(float deltaTime)
        {
            var count = _pending.Count;

            for (var i = 0; i < count; i++)
            {
                var pending = _pending.Dequeue();
                var remaining = pending.Remaining - deltaTime;

                if (remaining > 0f)
                {
                    _pending.Enqueue(new Pending(pending.Value, remaining));
                    continue;
                }

                Emit(pending.Value);
            }
        }

        /// <inheritdoc/>
        protected override void Reset() =>
            _pending.Clear();

        private readonly struct Pending
        {
            public readonly TValue Value;
            public readonly float Remaining;

            public Pending(TValue value, float remaining)
            {
                Value = value;
                Remaining = remaining;
            }
        }
    }
}
