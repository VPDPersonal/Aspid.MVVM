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

        [Tooltip("Inverts the bound value before it is applied.")]
        [SerializeField] private bool _isInvert;

        /// <summary>
        /// Sets <see cref="Screen.fullScreen"/>, inverting the value first when the Invert option is set.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(bool value) =>
            Screen.fullScreen = _isInvert ? !value : value;

        /// <summary>
        /// Called when the binder is bound. Sends the current state to the ViewModel when using
        /// <see cref="BindMode.OneWayToSource"/>.
        /// </summary>
        /// <remarks>The Invert option applies in this direction too.</remarks>
        protected override void OnBound()
        {
            if (Mode is not BindMode.OneWayToSource) return;

            var fullScreen = Screen.fullScreen;
            ValueChanged?.Invoke(_isInvert ? !fullScreen : fullScreen);
        }
    }
}
