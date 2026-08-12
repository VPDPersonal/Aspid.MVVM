using UnityEngine;
using UnityEngine.Events;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="MonoBinder"/> that eases each value it receives toward the previous one and forwards
    /// every intermediate value to a target <see cref="UnityEvent{T}"/>.
    /// </summary>
    /// <remarks>
    /// There was no interpolation anywhere in the package: a health bar bound to a health value jumped, and the usual
    /// fix — holding a second, animated value in the ViewModel and driving it from an update loop — puts frame-rate
    /// concerns into the layer that is supposed to be free of them.
    /// <para/>
    /// This is a caster rather than a property binder: it takes a value and emits values, so the same tween drives an
    /// image fill, a text number or anything else through the event. A new value while a tween is running retargets it
    /// from where it currently is, which is what makes a bar chase a moving number instead of restarting.
    /// <para/>
    /// A duration of zero forwards the value immediately, and so does the first value after binding — there is nothing
    /// to ease from yet, and easing from a type's default would flash the bar from empty.
    /// </remarks>
    /// <typeparam name="TValue">The type of value being eased.</typeparam>
    public abstract partial class TweenMonoBinder<TValue> : MonoBinder, IBinder<TValue>
    {
        [Tooltip("Invoked with every intermediate value while the tween runs, and with the final value when it ends.")]
        [SerializeField] private UnityEvent<TValue> _value;

        [Tooltip("Seconds the tween takes. Zero forwards each value immediately.")]
        [SerializeField] [Min(0f)] private float _duration = 0.25f;

        [Tooltip("Use unscaled time, so the tween keeps running while the game is paused through Time.timeScale.")]
        [SerializeField] private bool _isUnscaledTime = true;

        private TValue _from;
        private TValue _to;
        private TValue _current;
        private float _elapsed;
        private bool _isTweening;
        private bool _hasValue;

        /// <summary>
        /// Starts easing toward <paramref name="value"/> from whatever the tween currently shows.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(TValue value)
        {
            if (!_hasValue || _duration <= 0f)
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

        /// <summary>
        /// Called when the binder is unbound. Stops the tween so the next binding starts from the value it is given
        /// rather than easing out of a previous view's state.
        /// </summary>
        protected override void OnUnbound()
        {
            _isTweening = false;
            _hasValue = false;
        }

        /// <summary>
        /// Advances the tween and forwards the interpolated value.
        /// </summary>
        private void Update()
        {
            if (!_isTweening) return;

            _elapsed += _isUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;

            var progress = BinderMath.SafeClamp01(_elapsed / _duration);
            _current = Interpolate(_from, _to, progress);

            _value?.Invoke(_current);

            if (progress >= 1f) _isTweening = false;
        }

        /// <summary>
        /// Interpolates between <paramref name="from"/> and <paramref name="to"/>.
        /// </summary>
        /// <param name="from">The value the tween started at.</param>
        /// <param name="to">The value the tween is heading for.</param>
        /// <param name="progress">How far along the tween is, from 0 to 1.</param>
        /// <returns>The value to forward this frame.</returns>
        protected abstract TValue Interpolate(TValue from, TValue to, float progress);
    }
}
