#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="Binder"/> implementing <see cref="IBinder{T}">IBinder&lt;bool&gt;</see> and
    /// <see cref="IReverseBinder{T}">IReverseBinder&lt;bool&gt;</see> that binds
    /// <see cref="AudioListener.pause"/>.
    /// </summary>
    /// <remarks>
    /// Silences every source at once while keeping their playback positions, which is what a pause menu wants and what
    /// setting <see cref="Time.timeScale"/> to zero does not do — audio ignores the time scale. Like
    /// <see cref="AudioListener.volume"/> it is a static property, so the binder has no target.
    /// </remarks>
    [Serializable]
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource)]
    public class AudioListenerPauseBinder : Binder, IBinder<bool>, IReverseBinder<bool>
    {
        /// <inheritdoc/>
        public event Action<bool>? ValueChanged;

        [Tooltip("When enabled, the bound value is inverted before it is applied — bind an IsPlaying flag to it directly.")]
        [SerializeField] private bool _isInvert;

        /// <summary>
        /// Initializes a new instance of <see cref="AudioListenerPauseBinder"/>.
        /// </summary>
        /// <param name="isInvert">When <see langword="true"/>, the bound value is inverted before it is applied.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/> — the property raises no change event to listen to.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public AudioListenerPauseBinder(bool isInvert = false, BindMode mode = BindMode.OneWay)
            : base(mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
            _isInvert = isInvert;
        }

        /// <summary>
        /// Sets <see cref="AudioListener.pause"/>, inverting the value first when the Invert option is set.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        public void SetValue(bool value) =>
            AudioListener.pause = _isInvert ? !value : value;

        /// <summary>
        /// Called when the binder is bound. Sends the current state to the ViewModel when using
        /// <see cref="BindMode.OneWayToSource"/>.
        /// </summary>
        /// <remarks>
        /// The Invert option applies in this direction too, so the value the ViewModel receives is the one it would
        /// have had to send to produce the current state.
        /// </remarks>
        protected override void OnBound()
        {
            if (Mode is not BindMode.OneWayToSource) return;

            var paused = AudioListener.pause;
            ValueChanged?.Invoke(_isInvert ? !paused : paused);
        }
    }
}
