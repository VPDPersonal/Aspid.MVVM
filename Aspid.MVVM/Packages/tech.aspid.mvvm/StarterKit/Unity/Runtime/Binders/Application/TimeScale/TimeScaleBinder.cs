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
    /// Pause, slow motion and fast forward, which every game expresses through this one number — and which needed a
    /// MonoBehaviour of its own because the value belongs to no component.
    /// <para/>
    /// Clamped non-negative: Unity refuses a negative time scale and logs an error for it. A non-finite value lands
    /// on zero, which pauses the game rather than leaving it with a delta time no physics step can use.
    /// <para/>
    /// Audio does not follow the time scale — <see cref="AudioListenerPauseMonoBinder"/> is what silences a paused
    /// game.
    /// </remarks>
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource)]
    [Serializable]
    public class TimeScaleBinder : Binder, INumberBinder, IReverseBinder<float>
    {
        /// <inheritdoc/>
        public event Action<float>? ValueChanged;

        /// <summary>
        /// Initializes a new instance of <see cref="TimeScaleBinder"/>.
        /// </summary>
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
