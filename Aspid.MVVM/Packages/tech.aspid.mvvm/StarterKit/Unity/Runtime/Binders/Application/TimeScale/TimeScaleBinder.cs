#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="Binder"/> implementing <see cref="INumberBinder"/> and
    /// <see cref="IReverseBinder{T}">IReverseBinder&lt;float&gt;</see> that binds <see cref="Time.timeScale"/>.
    /// </summary>
    /// <remarks>
    /// Negative and non-finite values are clamped to zero, which pauses the game rather than being rejected. Audio
    /// does not follow the time scale — see <see cref="AudioListenerPauseMonoBinder"/> to silence a paused game.
    /// </remarks>
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource)]
    [Serializable]
    public class TimeScaleBinder : Binder, INumberBinder, IReverseBinder<float>
    {
        /// <inheritdoc/>
        public event Action<float>? ValueChanged;

        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/> — the value raises no change event to listen to.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public TimeScaleBinder(BindMode mode = BindMode.OneWay)
            : base(mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }

        /// <summary>
        /// Casts the value to <see langword="float"/> and applies it.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        public void SetValue(int value) => SetValue((float)value);

        /// <inheritdoc cref="SetValue(int)"/>
        public void SetValue(long value) => SetValue((float)value);

        /// <inheritdoc cref="SetValue(int)"/>
        /// <remarks>
        /// Narrowed to <see langword="float"/> — precision may be lost.
        /// </remarks>
        public void SetValue(double value) => SetValue((float)value);

        /// <summary>
        /// Applies the value.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        public void SetValue(float value)
        {
            Time.timeScale = BinderMath.SafeClamp(value, 0f, float.MaxValue);
        }

        /// <summary>
        /// Called when the binder is bound. Sends the current value to the ViewModel when using
        /// <see cref="BindMode.OneWayToSource"/>.
        /// </summary>
        protected override void OnBound()
        {
            if (Mode is BindMode.OneWayToSource)
                ValueChanged?.Invoke(Time.timeScale);
        }
    }
}
