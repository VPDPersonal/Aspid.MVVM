#nullable enable
using System;
using UnityEngine;
using UnityEngine.Audio;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetFloatBinder{TTarget}">TargetFloatBinder&lt;AudioMixer&gt;</see> that binds an exposed
    /// <see cref="AudioMixer"/> parameter.
    /// </summary>
    /// <remarks>
    /// The value is written to the parameter unchanged; mixer volumes are typically in decibels, so a linear 0..1
    /// slider needs a converter.
    /// <para/>
    /// Reads and writes are logged as errors and dropped when the name is blank, the value is non-finite, or no
    /// parameter of that name is exposed.
    /// </remarks>
    [Serializable]
    public class AudioMixerFloatBinder : TargetFloatBinder<AudioMixer>
    {
        [Tooltip("Exposed parameter name, exactly as in the mixer's Exposed Parameters list.")]
        [SerializeField] private string _parameter;

        /// <param name="target">The mixer that exposes the parameter.</param>
        /// <param name="parameter">Exposed parameter name, exactly as in the mixer's Exposed Parameters list.</param>
        /// <param name="converter">
        /// An optional converter applied to the value before it is written. Pass <see langword="null"/> to use the
        /// value unchanged. Runs in reverse only if it implements <see cref="ITwoWayConverter{TFrom, TTo}"/>.
        /// </param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/> — a mixer parameter raises no change event to listen to.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
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
            get => TryGetParameter(out var value) ? value : default;
            set
            {
                if (!IsUsable()) return;

                if (!BinderMath.IsFinite(value))
                {
                    this.LogError($"the value {value.Describe()} is not finite", "The parameter is left unchanged.", Target);
                    return;
                }

                // SetFloat returns false silently on an unmatched parameter name — the only way to catch a typo.
                if (!Target.SetFloat(_parameter, value))
                    this.LogError($"the mixer exposes no parameter {_parameter.Describe()}", "The value is not applied.", Target);
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
            if (Target.GetFloat(_parameter, out value)) return true;

            this.LogError($"the mixer exposes no parameter {_parameter.Describe()}", "No value is read from the mixer.", Target);
            return false;
        }

        private bool IsUsable()
        {
            if (!string.IsNullOrWhiteSpace(_parameter)) return true;

            this.LogError("no parameter name is set", "The mixer is left unchanged.", Target);
            return false;
        }
    }
}
