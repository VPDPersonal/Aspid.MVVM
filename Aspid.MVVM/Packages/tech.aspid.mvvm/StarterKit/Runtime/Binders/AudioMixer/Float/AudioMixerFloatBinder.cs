#nullable enable
using System;
using UnityEngine;
using UnityEngine.Audio;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetFloatBinder{TTarget}"/> that binds an exposed <see cref="AudioMixer"/> parameter.
    /// </summary>
    /// <remarks>
    /// The value is written as-is; mixer volumes are usually decibels. A blank name, an unexposed parameter and a
    /// non-finite value are reported and skipped.
    /// </remarks>
    [Serializable]
    public class AudioMixerFloatBinder : TargetFloatBinder<AudioMixer>
    {
        [Tooltip("Exposed parameter name.")]
        [SerializeField] private string _parameter;

        /// <param name="target">The mixer that exposes the parameter.</param>
        /// <param name="parameter">The exposed parameter name.</param>
        /// <param name="converter">
        /// The converter applied to the bound value, or <see langword="null"/> to use it as-is.
        /// </param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="ArgumentException"><paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public AudioMixerFloatBinder(
            AudioMixer target,
            string parameter,
            IConverter<float, float>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
            _parameter = parameter;
        }

        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => TryGetParameter(out var value) ? value : 0;
            set
            {
                if (!IsUsable()) return;

                if (!BinderMath.IsFinite(value))
                {
                    this.LogError(
                        problem: $"the value {value.Describe()} is not finite",
                        consequence: "The parameter is left unchanged.", Target);

                    return;
                }

                if (!Target.SetFloat(_parameter, value))
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
            value = 0;
            if (!IsUsable()) return false;
            if (Target.GetFloat(_parameter, out value)) return true;

            LogUnexposed("No value is read from the mixer.");
            return false;
        }

        private bool IsUsable()
        {
            if (!string.IsNullOrWhiteSpace(_parameter)) return true;

            this.LogError(
                problem: "no parameter name is set",
                consequence: "The mixer is left unchanged.", Target);

            return false;
        }

        private void LogUnexposed(string consequence) =>
            this.LogError(
                problem: $"the mixer exposes no parameter {_parameter.Describe()}",
                consequence: consequence, Target);
    }
}
