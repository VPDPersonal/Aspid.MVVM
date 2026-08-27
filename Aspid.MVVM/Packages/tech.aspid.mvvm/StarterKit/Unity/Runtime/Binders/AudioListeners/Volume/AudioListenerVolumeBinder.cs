#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="Binder"/> implementing <see cref="INumberBinder"/> and
    /// <see cref="IReverseBinder{T}">IReverseBinder&lt;float&gt;</see> that binds
    /// <see cref="AudioListener.volume"/>.
    /// </summary>
    /// <remarks>
    /// The master volume of the whole game, and the one audio value that is not attached to anything: it is a static
    /// property, so this binder has no target. A project without an <see cref="AudioMixer"/> has nothing else to bind
    /// a master slider to.
    /// <para/>
    /// Clamped to 0..1, the range Unity documents; a non-finite value lands on zero rather than silencing the game
    /// with nothing in the log.
    /// </remarks>
    [Serializable]
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource)]
    public class AudioListenerVolumeBinder : Binder, INumberBinder, IReverseBinder<float>
    {
        /// <inheritdoc/>
        public event Action<float>? ValueChanged;

        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/> — the volume raises no change event to listen to.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public AudioListenerVolumeBinder(BindMode mode = BindMode.OneWay)
            : base(mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }

        /// <summary>
        /// Casts the value to <see langword="float"/> and sets <see cref="AudioListener.volume"/>.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        public void SetValue(int value) =>
            SetValue((float)value);

        /// <inheritdoc cref="SetValue(int)"/>
        public void SetValue(long value) =>
            SetValue((float)value);

        /// <inheritdoc cref="SetValue(int)"/>
        /// <remarks>
        /// Narrowed to <see langword="float"/> — precision may be lost.
        /// </remarks>
        public void SetValue(double value) =>
            SetValue((float)value);

        /// <summary>
        /// Sets <see cref="AudioListener.volume"/>, clamped to 0..1.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        public void SetValue(float value) =>
            AudioListener.volume = BinderMath.SafeClamp01(value);

        /// <summary>
        /// Called when the binder is bound. Sends the current volume to the ViewModel when using
        /// <see cref="BindMode.OneWayToSource"/>.
        /// </summary>
        protected override void OnBound()
        {
            if (Mode is BindMode.OneWayToSource)
                ValueChanged?.Invoke(AudioListener.volume);
        }
    }
}
