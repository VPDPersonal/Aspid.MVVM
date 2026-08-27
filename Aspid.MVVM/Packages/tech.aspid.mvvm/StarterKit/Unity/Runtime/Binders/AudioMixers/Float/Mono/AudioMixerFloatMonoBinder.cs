using UnityEngine;
using UnityEngine.Audio;

// ReSharper disable once CheckNamespace
// ReSharper disable NotNullOrRequiredMemberIsNotInitialized
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="FloatMonoBinder"/> that binds an exposed <see cref="AudioMixer"/> parameter.
    /// </summary>
    /// <remarks>
    /// The value is written to the parameter unchanged; mixer volumes are typically in decibels, so a linear 0..1
    /// slider needs a converter.
    /// <para/>
    /// Reads and writes are logged as errors and dropped when the mixer is missing, the name is blank, the value is
    /// non-finite, or no parameter of that name is exposed.
    /// </remarks>
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioMixer Binder – Float")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/Audio/AudioMixer Binder – Float")]
    public class AudioMixerFloatMonoBinder : FloatMonoBinder
    {
        [Tooltip("The mixer that exposes the parameter. Required — logs an error if missing.")]
        [SerializeField] private AudioMixer _mixer;

        [Tooltip("Exposed parameter name, exactly as in the mixer's Exposed Parameters list.")]
        [SerializeField] private string _parameter;

        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => TryGetParameter(out var value) ? value : default;
            set
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
        }

        /// <summary>
        /// Sends the parameter's current value to the ViewModel, and nothing at all when it cannot be read.
        /// </summary>
        protected override void SendInitialValueToSource()
        {
            if (TryGetParameter(out _))
                base.SendInitialValueToSource();
        }

        private bool TryGetParameter(out float value)
        {
            value = default;
            if (!IsUsable()) return false;
            if (_mixer.GetFloat(_parameter, out value)) return true;

            Debug.LogError($"[{nameof(AudioMixerFloatMonoBinder)}] Mixer '{_mixer.name}' exposes no parameter '{_parameter}'.", context: this);
            return false;
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
