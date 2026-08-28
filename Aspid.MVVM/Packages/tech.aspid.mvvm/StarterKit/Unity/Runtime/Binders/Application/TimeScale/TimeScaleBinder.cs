#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="FloatBinder"/> that binds <see cref="Time.timeScale"/>.
    /// </summary>
    /// <remarks>
    /// Negative and non-finite values are clamped to zero, which pauses the game rather than being rejected. Audio
    /// does not follow the timescale — see <see cref="AudioListenerPauseMonoBinder"/> to silence a paused game.
    /// </remarks>
    [Serializable]
    public class TimeScaleBinder : FloatBinder
    {
        /// <param name="converter">
        /// An optional converter applied to the timescale before it is applied. Pass <see langword="null"/> to use the
        /// value unchanged. Runs in reverse only if it implements <see cref="ITwoWayConverter{TFrom, TTo}"/>.
        /// </param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/> — the value raises no change event to listen to.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public TimeScaleBinder(IConverter<float, float>? converter = null, BindMode mode = BindMode.OneWay)
            : base(converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }

        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => Time.timeScale;
            set => Time.timeScale = this.SafeClamp(value, 0f, float.MaxValue);
        }
    }
}
