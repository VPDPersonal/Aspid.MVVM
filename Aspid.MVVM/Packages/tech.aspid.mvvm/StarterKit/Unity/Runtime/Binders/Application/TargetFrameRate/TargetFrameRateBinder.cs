#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="IntBinder"/> that binds <see cref="Application.targetFrameRate"/>.
    /// </summary>
    /// <remarks>
    /// Values below <c>-1</c> are clamped to <c>-1</c>, which hands the decision back to the platform. When
    /// <see cref="QualitySettings.vSyncCount"/> is not zero, vsync wins and the cap is ignored.
    /// </remarks>
    [Serializable]
    public class TargetFrameRateBinder : IntBinder
    {
        /// <param name="converter">
        /// An optional converter applied to the frame cap before it is applied. Pass <see langword="null"/> to use the
        /// value unchanged. Runs in reverse only if it implements <see cref="ITwoWayConverter{TFrom, TTo}"/>.
        /// </param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/> — the value raises no change event to listen to.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public TargetFrameRateBinder(IConverter<int, int>? converter = null, BindMode mode = BindMode.OneWay)
            : base(converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }

        /// <inheritdoc/>
        protected sealed override int Property
        {
            get => Application.targetFrameRate;
            set => Application.targetFrameRate = Mathf.Max(-1, value);
        }
    }
}
