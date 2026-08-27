using System;
using UnityEngine;
using UnityEngine.Audio;

// ReSharper disable once CheckNamespace
// ReSharper disable NotNullOrRequiredMemberIsNotInitialized
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder"/> implementing <see cref="INumberBinder"/> and <see cref="IReverseBinder{T}">IReverseBinder&lt;float&gt;</see>
    /// that writes an exposed <see cref="AudioMixer"/> parameter.
    /// </summary>
    /// <remarks>
    /// The value is written to the parameter unchanged; mixer volumes are typically in decibels, so a linear 0..1
    /// slider needs a converter.
    /// </remarks>
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource)]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioMixer Binder – Float")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/Audio/AudioMixer Binder – Float")]
    public partial class AudioMixerFloatMonoBinder : MonoBinder, INumberBinder, IReverseBinder<float>
    {
        /// <inheritdoc/>
        public event Action<float> ValueChanged;

        [Tooltip("The mixer that exposes the parameter. Required — logs an error if missing.")]
        [SerializeField] private AudioMixer _mixer;

        [Tooltip("Exposed parameter name, exactly as in the mixer's Exposed Parameters list.")]
        [SerializeField] private string _parameter;

        /// <summary>
        /// Casts the value to <see langword="float"/> and writes the exposed parameter.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(int value) =>
            SetValue((float)value);

        /// <inheritdoc cref="SetValue(int)"/>
        [BinderLog]
        public void SetValue(long value) =>
            SetValue((float)value);

        /// <inheritdoc cref="SetValue(int)"/>
        /// <remarks>
        /// Narrowed to <see langword="float"/> — precision may be lost.
        /// </remarks>
        [BinderLog]
        public void SetValue(double value) =>
            SetValue((float)value);

        /// <summary>
        /// Writes the exposed parameter named by the Inspector.
        /// </summary>
        /// <param name="value">The value received from the ViewModel, in the parameter's own units.</param>
        /// <remarks>
        /// Logs an error and writes nothing when the mixer is missing, the name is blank, the value is non-finite,
        /// or the mixer refuses the write — which it does when no parameter of that name is exposed.
        /// </remarks>
        [BinderLog]
        public void SetValue(float value)
        {
            if (!IsUsable()) return;

            if (!BinderMath.IsFinite(value))
            {
                Debug.LogError($"[{nameof(AudioMixerFloatMonoBinder)}] Non-finite value ignored for parameter '{_parameter}'.", context: this);
                return;
            }

            // SetFloat returns false silently on an unmatched parameter name — the only way to catch a typo.
            if (!_mixer.SetFloat(_parameter, value))
                Debug.LogError($"[{nameof(AudioMixerFloatMonoBinder)}] Mixer '{_mixer.name}' exposes no parameter '{_parameter}'.", context: this);
        }

        /// <summary>
        /// Called when the binder is bound. Sends the parameter's current value to the ViewModel when using
        /// <see cref="BindMode.OneWayToSource"/>.
        /// </summary>
        protected override void OnBound()
        {
            if (Mode is not BindMode.OneWayToSource) return;
            if (!IsUsable()) return;

            if (_mixer.GetFloat(_parameter, out var value)) ValueChanged?.Invoke(value);
            else Debug.LogError($"[{nameof(AudioMixerFloatMonoBinder)}] Mixer '{_mixer.name}' exposes no parameter '{_parameter}'.", context: this);
        }

        private bool IsUsable()
        {
            if (!_mixer)
            {
                Debug.LogError($"[{nameof(AudioMixerFloatMonoBinder)}] No mixer assigned.", context: this);
                return false;
            }

            if (string.IsNullOrWhiteSpace(_parameter))
            {
                Debug.LogError($"[{nameof(AudioMixerFloatMonoBinder)}] No parameter name set.", context: this);
                return false;
            }

            return true;
        }
    }
}
