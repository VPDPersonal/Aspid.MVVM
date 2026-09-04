using UnityEngine;
using UnityEngine.Events;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract <see cref="MonoBinder"/> that eases toward each received value and forwards every intermediate value
    /// to a <see cref="UnityEvent{T}"/>.
    /// </summary>
    /// <remarks>
    /// A new value retargets a running tween from its current position. The first value after binding, and every
    /// value with a zero duration, is forwarded at once.
    /// </remarks>
    /// <typeparam name="TValue">The type of the eased value.</typeparam>
    public abstract partial class TweenMonoBinder<TValue> : MonoBinder, IBinder<TValue>
    {
        [Tooltip("Invoked with every intermediate and the final value.")]
        [SerializeField] private UnityEvent<TValue> _value;

        [Tooltip("Tween duration in seconds; zero forwards immediately.")]
        [SerializeField] [Min(0f)] private float _duration = 0.25f;

        [Tooltip("Use unscaled time to keep tweening while the game is paused.")]
        [SerializeField] private bool _isUnscaledTime = true;

        private TValue _from;
        private TValue _to;
        private TValue _current;
        private float _elapsed;
        private bool _isTweening;
        private bool _hasValue;

        /// <summary>
        /// Starts easing toward <paramref name="value"/> from the value currently shown.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(TValue value)
        {
            if (!_hasValue || _duration is 0f)
            {
                _hasValue = true;
                _isTweening = false;
                _current = value;
                _value?.Invoke(value);
                return;
            }

            _from = _current;
            _to = value;
            _elapsed = 0f;
            _isTweening = true;
        }

        /// <inheritdoc/>
        protected override void OnUnbound()
        {
            _isTweening = false;
            _hasValue = false;
        }

        /// <summary>
        /// Interpolates between <paramref name="from"/> and <paramref name="to"/>.
        /// </summary>
        /// <param name="from">The start value.</param>
        /// <param name="to">The target value.</param>
        /// <param name="progress">The progress in [0, 1].</param>
        /// <returns>The value to forward this frame.</returns>
        protected abstract TValue Interpolate(TValue from, TValue to, float progress);

        private void Update()
        {
            if (!_isTweening) return;

            _elapsed += _isUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
            var progress = this.SafeClamp01(_elapsed / _duration);

            _current = Interpolate(_from, _to, progress);
            _value?.Invoke(_current);

            if (progress >= 1f) _isTweening = false;
        }
    }
}
