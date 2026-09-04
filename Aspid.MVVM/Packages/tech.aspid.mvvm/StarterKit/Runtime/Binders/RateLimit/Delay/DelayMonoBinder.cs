using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract <see cref="RateLimitedMonoBinder{TValue}"/> that forwards every value after the interval, in arrival
    /// order.
    /// </summary>
    /// <remarks>
    /// Values still queued on unbind are dropped.
    /// </remarks>
    /// <typeparam name="TValue">The type of the forwarded value.</typeparam>
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

                if (remaining > 0f) _pending.Enqueue(new Pending(pending.Value, remaining));
                else Emit(pending.Value);
            }
        }

        /// <inheritdoc/>
        protected override void Clear() =>
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
