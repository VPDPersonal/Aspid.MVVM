using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder"/> implementing <see cref="IBinder{T}">IBinder&lt;bool&gt;</see> and
    /// <see cref="IReverseBinder{T}">IReverseBinder&lt;bool&gt;</see> that binds
    /// <see cref="AudioListener.pause"/>.
    /// </summary>
    /// <remarks>
    /// Silences every source at once while keeping their playback positions, which is what a pause menu wants and
    /// what setting <see cref="Time.timeScale"/> to zero does not do — audio ignores the time scale. Like
    /// <see cref="AudioListener.volume"/> it is a static property, so the binder needs no target.
    /// </remarks>
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource)]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioListener Binder – Pause")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/Audio/AudioListener Binder – Pause")]
    public partial class AudioListenerPauseMonoBinder : MonoBinder, IBinder<bool>, IReverseBinder<bool>
    {
        /// <inheritdoc/>
        public event Action<bool> ValueChanged;

        [Tooltip("Optional converter applied to the value; runs in reverse only via ITwoWayConverter.")]
        [SerializeReference] private IConverter<bool, bool> _converter;

        /// <summary>
        /// Sets <see cref="AudioListener.pause"/>, applying the configured converter if present.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(bool value) =>
            AudioListener.pause = _converter?.Convert(value) ?? value;

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

            var paused = AudioListener.pause;
            ValueChanged?.Invoke(_converter is ITwoWayConverter<bool, bool> twoWay ? twoWay.ConvertBack(paused) : paused);
        }
    }
}
