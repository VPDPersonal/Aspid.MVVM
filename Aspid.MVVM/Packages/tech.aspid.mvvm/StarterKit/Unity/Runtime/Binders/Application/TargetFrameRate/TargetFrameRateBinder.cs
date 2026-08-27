#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="Binder"/> implementing <see cref="INumberBinder"/> and
    /// <see cref="IReverseBinder{T}">IReverseBinder&lt;int&gt;</see> that binds <see cref="Application.targetFrameRate"/>.
    /// </summary>
    /// <remarks>
    /// Values below <c>-1</c> are clamped to <c>-1</c>, which hands the decision back to the platform. When
    /// <see cref="QualitySettings.vSyncCount"/> is not zero, vsync wins and the cap is ignored.
    /// </remarks>
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource)]
    [Serializable]
    public class TargetFrameRateBinder : Binder, INumberBinder, IReverseBinder<int>
    {
        /// <inheritdoc/>
        public event Action<int>? ValueChanged;

        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/> — the value raises no change event to listen to.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public TargetFrameRateBinder(BindMode mode = BindMode.OneWay)
            : base(mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }

        /// <summary>
        /// Casts the value to <see langword="int"/> and applies it.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        public void SetValue(long value) => SetValue((int)value);

        /// <inheritdoc cref="SetValue(long)"/>
        public void SetValue(float value) => SetValue((int)value);

        /// <inheritdoc cref="SetValue(long)"/>
        public void SetValue(double value) => SetValue((int)value);

        /// <summary>
        /// Applies the value.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        public void SetValue(int value)
        {
            Application.targetFrameRate = Mathf.Max(-1, value);
        }

        /// <summary>
        /// Called when the binder is bound. Sends the current value to the ViewModel when using
        /// <see cref="BindMode.OneWayToSource"/>.
        /// </summary>
        protected override void OnBound()
        {
            if (Mode is BindMode.OneWayToSource)
                ValueChanged?.Invoke(Application.targetFrameRate);
        }
    }
}
