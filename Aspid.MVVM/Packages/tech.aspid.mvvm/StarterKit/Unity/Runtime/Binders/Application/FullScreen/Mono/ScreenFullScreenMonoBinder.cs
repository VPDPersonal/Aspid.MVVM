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
    /// The windowed-or-fullscreen toggle every settings screen has. The value belongs to no component, so binding it
    /// used to mean a MonoBehaviour written for the purpose.
    /// <para/>
    /// Unity applies the change at the end of the frame, so reading the property back immediately still reports the old
    /// state — a ViewModel that shows the toggle from this binder's reverse channel sees the new value on the next
    /// binding, not the same one.
    /// <para/>
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established, the current state is sent to the
    /// ViewModel.
    /// </remarks>
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource)]
    [AddComponentMenu("Aspid/MVVM/Binders/Application/Application Binder – Full Screen")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/Application/FullScreen")]
    public partial class ScreenFullScreenMonoBinder : MonoBinder, IBinder<bool>, IReverseBinder<bool>
    {
        /// <inheritdoc/>
        public event Action<bool> ValueChanged;

        [Tooltip("When enabled, the bound value is inverted before it is applied — bind an IsWindowed flag to it directly.")]
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
        /// <remarks>
        /// The Invert option applies in this direction too, so the value the ViewModel receives is the one it would have
        /// had to send to produce the current state.
        /// </remarks>
        protected override void OnBound()
        {
            if (Mode is not BindMode.OneWayToSource) return;

            var fullScreen = Screen.fullScreen;
            ValueChanged?.Invoke(_isInvert ? !fullScreen : fullScreen);
        }
    }
}
