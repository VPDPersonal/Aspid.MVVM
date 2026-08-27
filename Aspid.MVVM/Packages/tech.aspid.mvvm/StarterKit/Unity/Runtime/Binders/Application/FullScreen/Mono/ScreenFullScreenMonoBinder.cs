using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder"/> implementing <see cref="IBinder{T}">IBinder&lt;bool&gt;</see> and
    /// <see cref="IReverseBinder{T}">IReverseBinder&lt;bool&gt;</see> that binds <see cref="Screen.fullScreen"/>.
    /// </summary>
    /// <remarks>
    /// Unity applies the change at the end of the frame, so reading the property back immediately still reports
    /// the old state.
    /// </remarks>
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource)]
    [AddComponentMenu("Aspid/MVVM/Binders/Application/Application Binder – Full Screen")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/Application/FullScreen")]
    public partial class ScreenFullScreenMonoBinder : MonoBinder, IBinder<bool>, IReverseBinder<bool>
    {
        /// <inheritdoc/>
        public event Action<bool> ValueChanged;

        [Tooltip("Optional converter applied to the value; runs in reverse only via ITwoWayConverter.")]
        [SerializeReference] private IConverter<bool, bool> _converter;

        /// <summary>
        /// Sets <see cref="Screen.fullScreen"/>, applying the configured converter if present.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
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
