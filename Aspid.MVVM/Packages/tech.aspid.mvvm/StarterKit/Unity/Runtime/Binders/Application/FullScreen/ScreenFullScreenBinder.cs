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

        [Tooltip("Optional converter applied to the value; runs in reverse only via ITwoWayConverter.")]
        [SerializeReference] private IConverter<bool, bool>? _converter;

        /// <param name="converter">
        /// An optional converter applied to the value before it is applied. Pass <see langword="null"/> to use the
        /// value unchanged. Runs in reverse only if it implements <see cref="ITwoWayConverter{TFrom, TTo}"/>.
        /// </param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/> — the property raises no change event to listen to.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public ScreenFullScreenBinder(IConverter<bool, bool>? converter = null, BindMode mode = BindMode.OneWay)
            : base(mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
            _converter = converter;
        }

        /// <summary>
        /// Sets <see cref="Screen.fullScreen"/>, applying the configured converter if present.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        public void SetValue(bool value) =>
            Screen.fullScreen = _converter?.Convert(value) ?? value;

        /// <summary>
        /// Called when the binder is bound. Sends the current state to the ViewModel when using
        /// <see cref="BindMode.OneWayToSource"/>.
        /// </summary>
        /// <remarks>
        /// The converter runs in this direction only when it implements <see cref="ITwoWayConverter{TFrom, TTo}"/>;
        /// otherwise the raw state is sent.
        /// </remarks>
        protected override void OnBound()
        {
            if (Mode is not BindMode.OneWayToSource) return;

            var fullScreen = _converter is ITwoWayConverter<bool, bool> twoWay
                ? twoWay.ConvertBack(Screen.fullScreen)
                : Screen.fullScreen;
            
            ValueChanged?.Invoke(fullScreen);
        }
    }
}
