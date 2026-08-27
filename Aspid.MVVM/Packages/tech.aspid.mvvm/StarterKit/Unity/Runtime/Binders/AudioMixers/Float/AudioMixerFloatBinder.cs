#nullable enable
using System;
using UnityEngine;
using UnityEngine.Audio;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{AudioMixer}"/> implementing <see cref="INumberBinder"/> and
    /// <see cref="IReverseBinder{T}">IReverseBinder&lt;float&gt;</see> that writes an exposed
    /// <see cref="AudioMixer"/> parameter.
    /// </summary>
    /// <remarks>
    /// The value is written to the parameter unchanged; mixer volumes are typically in decibels, so a linear 0..1
    /// slider needs a converter.
    /// </remarks>
    [Serializable]
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource)]
    public class AudioMixerFloatBinder : TargetBinder<AudioMixer>, INumberBinder, IReverseBinder<float>
    {
        /// <inheritdoc/>
        public event Action<float>? ValueChanged;

        [Tooltip("Exposed parameter name, exactly as in the mixer's Exposed Parameters list.")]
        [SerializeField] private string _parameter;

        /// <param name="target">The mixer that exposes the parameter.</param>
        /// <param name="parameter">Exposed parameter name, exactly as in the mixer's Exposed Parameters list.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/> — a mixer parameter raises no change event to listen to.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public AudioMixerFloatBinder(AudioMixer target, string parameter, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
            _parameter = parameter;
        }

        /// <summary>
        /// Casts the value to <see langword="float"/> and writes the exposed parameter.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        public void SetValue(int value) =>
            SetValue((float)value);

        /// <inheritdoc cref="SetValue(int)"/>
        public void SetValue(long value) =>
            SetValue((float)value);

        /// <inheritdoc cref="SetValue(int)"/>
        /// <remarks>
        /// Narrowed to <see langword="float"/> — precision may be lost.
        /// </remarks>
        public void SetValue(double value) =>
            SetValue((float)value);

        /// <summary>
        /// Writes the exposed parameter named at construction.
        /// </summary>
        /// <param name="value">The value received from the ViewModel, in the parameter's own units.</param>
        /// <remarks>
        /// Logs an error and writes nothing when the name is blank, the value is non-finite, or the mixer refuses the
        /// write — which it does when no parameter of that name is exposed.
        /// </remarks>
        public void SetValue(float value)
        {
            if (!IsUsable()) return;

            if (!BinderMath.IsFinite(value))
            {
                Debug.LogError($"[{nameof(AudioMixerFloatBinder)}] Non-finite value ignored for parameter '{_parameter}'.", Target);
                return;
            }

            // SetFloat returns false silently on an unmatched parameter name — the only way to catch a typo.
            if (!Target.SetFloat(_parameter, value))
                Debug.LogError($"[{nameof(AudioMixerFloatBinder)}] Mixer '{Target.name}' exposes no parameter '{_parameter}'.", Target);
        }

        /// <summary>
        /// Called when the binder is bound. Sends the parameter's current value to the ViewModel when using
        /// <see cref="BindMode.OneWayToSource"/>.
        /// </summary>
        protected override void OnBound()
        {
            if (Mode is not BindMode.OneWayToSource) return;
            if (!IsUsable()) return;

            if (Target.GetFloat(_parameter, out var value)) ValueChanged?.Invoke(value);
            else Debug.LogError($"[{nameof(AudioMixerFloatBinder)}] Mixer '{Target.name}' exposes no parameter '{_parameter}'.", Target);
        }

        private bool IsUsable()
        {
            if (!string.IsNullOrWhiteSpace(_parameter)) return true;

            Debug.LogError($"[{nameof(AudioMixerFloatBinder)}] No parameter name set.", Target);
            return false;
        }
    }
}
