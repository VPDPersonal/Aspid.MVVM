using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="RateLimitedMonoBinder{TValue}"/> that forwards every value, late.
    /// </summary>
    /// <remarks>
    /// Unlike a debounce or a throttle, nothing is dropped: each value is forwarded after the interval, in the order it
    /// arrived. That is what a staggered reveal needs — a list that fills in one row at a time, a combo counter that
    /// lands after the hit animation.
    /// <para/>
    /// Values waiting their turn are queued, so a burst costs one queue entry each. Releasing the binding drops the whole
    /// queue: a value that belonged to the previous binding must not arrive after it.
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
