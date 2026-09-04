using UnityEngine;
using UnityEngine.Events;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract <see cref="MonoBinder"/> that decides when a received value is forwarded to a
    /// <see cref="UnityEvent{T}"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of the forwarded value.</typeparam>
    public abstract partial class RateLimitedMonoBinder<TValue> : MonoBinder, IBinder<TValue>
    {
        [Tooltip("Invoked with each value the policy lets through.")]
        [SerializeField] private UnityEvent<TValue> _value;

        [Tooltip("Interval in seconds; zero forwards immediately.")]
        [SerializeField] [Min(0f)] private float _seconds = 0.25f;

        [Tooltip("Use unscaled time to keep working while the game is paused.")]
        [SerializeField] private bool _isUnscaledTime = true;

        /// <summary>
        /// Gets the interval in seconds.
        /// </summary>
        protected float Seconds => _seconds;

        /// <summary>
        /// Hands the value to the policy, or forwards it at once when the interval is zero.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(TValue value)
        {
            if (_seconds is 0f) Emit(value);
            else OnValue(value);
        }

        /// <inheritdoc/>
        protected override void OnUnbound() =>
            Clear();

        /// <summary>
        /// Forwards <paramref name="value"/> to the event.
        /// </summary>
        /// <param name="value">The value to forward.</param>
        protected void Emit(TValue value) =>
            _value?.Invoke(value);

        /// <summary>
        /// Receives a value while the interval is greater than zero.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        protected abstract void OnValue(TValue value);

        /// <summary>
        /// Advances the policy by one frame.
        /// </summary>
        /// <param name="deltaTime">Seconds since the previous frame.</param>
        protected abstract void Tick(float deltaTime);

        /// <summary>
        /// Drops whatever the policy is holding.
        /// </summary>
        protected abstract void Clear();

        private void Update()
        {
            if (IsBound)
                Tick(_isUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime);
        }
    }
}
