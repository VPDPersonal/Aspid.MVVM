using UnityEngine;
using UnityEngine.Events;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="MonoBinder"/> that decides <em>when</em> a received value is forwarded to a target
    /// <see cref="UnityEvent{T}"/>, without changing the value itself.
    /// </summary>
    /// <typeparam name="TValue">The type of value being forwarded.</typeparam>
    public abstract partial class RateLimitedMonoBinder<TValue> : MonoBinder, IBinder<TValue>
    {
        [Tooltip("Invoked with each value the policy lets through.")]
        [SerializeField] private UnityEvent<TValue> _value;

        [Tooltip("Interval the policy works with, in seconds. Zero forwards immediately.")]
        [SerializeField] [Min(0f)] private float _seconds = 0.25f;

        [Tooltip("Use unscaled time so the binder keeps working while the game is paused.")]
        [SerializeField] private bool _isUnscaledTime = true;

        /// <summary>
        /// Gets the interval the policy works with, in seconds.
        /// </summary>
        protected float Seconds => _seconds;

        /// <summary>
        /// Accepts a value from the ViewModel and hands it to the policy, or forwards it at once when the interval is
        /// zero.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(TValue value)
        {
            if (_seconds <= 0f)
            {
                Emit(value);
                return;
            }

            OnValue(value);
        }

        /// <summary>
        /// Called when the binder is unbound. Drops whatever the policy was holding, so a reused binder does not emit a
        /// value that belonged to the previous binding.
        /// </summary>
        protected override void OnUnbound() =>
            Reset();

        /// <summary>
        /// Forwards <paramref name="value"/> to the target event.
        /// </summary>
        /// <param name="value">The value to forward.</param>
        protected void Emit(TValue value) =>
            _value?.Invoke(value);

        /// <summary>
        /// Called for every value the ViewModel publishes while the interval is greater than zero.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        protected abstract void OnValue(TValue value);

        /// <summary>
        /// Called once per frame with the elapsed time. Override to advance the policy's own timing.
        /// </summary>
        /// <param name="deltaTime">Seconds since the previous frame, unscaled when the binder is configured that way.</param>
        protected abstract void Tick(float deltaTime);

        /// <summary>
        /// Called when the binding is released. Override to drop any value the policy is holding.
        /// </summary>
        protected abstract void Reset();

        private void Update()
        {
            if (!IsBound) return;
            Tick(_isUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime);
        }
    }
}
