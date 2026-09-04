using UnityEngine;
using UnityEngine.Audio;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="FloatMonoBinder"/> that binds an exposed <see cref="AudioMixer"/> parameter.
    /// </summary>
    /// <remarks>
    /// The value is written as-is; mixer volumes are usually decibels. A blank name, an unexposed parameter and a
    /// non-finite value are reported and skipped.
    /// </remarks>
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioMixer Binder – Float")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/Audio/AudioMixer Binder – Float")]
    public class AudioMixerFloatMonoBinder : FloatMonoBinder
    {
        [Tooltip("Mixer that exposes the parameter.")]
        [SerializeField] private AudioMixer _mixer;

        [Tooltip("Exposed parameter name.")]
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
                    this.LogError(
                        problem: $"the value {value.Describe()} is not finite",
                        consequence: "The parameter is left unchanged.");

                    return;
                }

                if (!_mixer.SetFloat(_parameter, value))
                    LogUnexposed("The value is not applied.");
            }
        }

        /// <inheritdoc/>
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

            LogUnexposed("No value is read from the mixer.");
            return false;
        }

        private bool IsUsable()
        {
            if (!_mixer)
            {
                this.LogError(
                    problem: "no mixer is assigned",
                    consequence: "The binder does nothing.");

                return false;
            }

            if (!string.IsNullOrWhiteSpace(_parameter)) return true;

            this.LogError(
                problem: "no parameter name is set",
                consequence: "The mixer is left unchanged.");

            return false;
        }

        private void LogUnexposed(string consequence) =>
            this.LogError(
                problem: $"the mixer exposes no parameter {_parameter.Describe()}",
                consequence: consequence);
    }
}
