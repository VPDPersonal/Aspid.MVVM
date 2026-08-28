#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="FloatBinder"/> that binds <see cref="AudioListener.volume"/>.
    /// </summary>
    /// <remarks>
    /// The master volume of the whole game, and the one audio value that is not attached to anything: it is a static
    /// property, so this binder has no target. A project without an <see cref="AudioMixer"/> has nothing else to bind
    /// a master slider to.
    /// <para/>
    /// Clamped to 0..1, the range Unity documents; a non-finite value lands on zero rather than silencing the game
    /// with nothing in the log.
    /// </remarks>
    [Serializable]
    public class AudioListenerVolumeBinder : FloatBinder
    {
        /// <param name="converter">
        /// An optional converter applied to the volume before it is applied. Pass <see langword="null"/> to use the
        /// value unchanged. Runs in reverse only if it implements <see cref="ITwoWayConverter{TFrom, TTo}"/>.
        /// </param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/> — the volume raises no change event to listen to.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public AudioListenerVolumeBinder(IConverter<float, float>? converter = null, BindMode mode = BindMode.OneWay)
            : base(converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }

        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => AudioListener.volume;
            set => AudioListener.volume = this.SafeClamp01(value);
        }
    }
}
