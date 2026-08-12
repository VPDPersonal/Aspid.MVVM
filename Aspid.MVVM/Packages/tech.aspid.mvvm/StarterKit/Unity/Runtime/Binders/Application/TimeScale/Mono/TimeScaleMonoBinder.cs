using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder"/> implementing <see cref="INumberBinder"/> and
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
    [AddComponentMenu("Aspid/MVVM/Binders/Application/Application Binder – Time Scale")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/Application/TimeScale")]
    public partial class TimeScaleMonoBinder : MonoBinder, INumberBinder, IReverseBinder<float>
    {
        /// <inheritdoc/>
        public event Action<float> ValueChanged;

        /// <summary>
        /// Casts the value to <see langword="float"/> and applies it.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(int value) => SetValue((float)value);

        /// <inheritdoc cref="SetValue(int)"/>
        [BinderLog]
        public void SetValue(long value) => SetValue((float)value);

        /// <inheritdoc cref="SetValue(int)"/>
        /// <remarks>
        /// Narrowed to <see langword="float"/> — precision may be lost.
        /// </remarks>
        [BinderLog]
        public void SetValue(double value) => SetValue((float)value);

        /// <summary>
        /// Applies the value.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
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
