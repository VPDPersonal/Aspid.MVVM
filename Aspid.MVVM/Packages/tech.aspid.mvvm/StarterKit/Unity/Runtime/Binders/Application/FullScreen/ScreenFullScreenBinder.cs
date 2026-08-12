#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="Binder"/> implementing <see cref="IBinder{T}">IBinder&lt;bool&gt;</see> and
    /// <see cref="IReverseBinder{T}">IReverseBinder&lt;bool&gt;</see> that binds <see cref="Screen.fullScreen"/>.
    /// </summary>
    /// <remarks>
    /// The windowed-or-fullscreen toggle every settings screen has. Unity applies the change at the end of the frame,
    /// so reading the property back immediately still reports the old state.
    /// </remarks>
    [Serializable]
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource)]
    public class ScreenFullScreenBinder : Binder, IBinder<bool>, IReverseBinder<bool>
    {
        /// <inheritdoc/>
        public event Action<bool>? ValueChanged;

        [Tooltip("When enabled, the bound value is inverted before it is applied — bind an IsWindowed flag to it directly.")]
        [SerializeField] private bool _isInvert;

        /// <summary>
        /// Initializes a new instance of <see cref="ScreenFullScreenBinder"/>.
        /// </summary>
        /// <param name="isInvert">When <see langword="true"/>, the bound value is inverted before it is applied.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/> — the property raises no change event to listen to.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public ScreenFullScreenBinder(bool isInvert = false, BindMode mode = BindMode.OneWay)
            : base(mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
            _isInvert = isInvert;
        }

        /// <summary>
        /// Sets <see cref="Screen.fullScreen"/>, inverting the value first when the Invert option is set.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        public void SetValue(bool value) =>
            Screen.fullScreen = _isInvert ? !value : value;

        /// <summary>
        /// Called when the binder is bound. Sends the current state to the ViewModel when using
        /// <see cref="BindMode.OneWayToSource"/>.
        /// </summary>
        protected override void OnBound()
        {
            if (Mode is not BindMode.OneWayToSource) return;

            var fullScreen = Screen.fullScreen;
            ValueChanged?.Invoke(_isInvert ? !fullScreen : fullScreen);
        }
    }
}
