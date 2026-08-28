#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="IntBinder"/> that binds the active <see cref="QualitySettings"/> level.
    /// </summary>
    /// <remarks>
    /// Clamped to the range of levels the project defines, rather than letting Unity throw on an out-of-range
    /// index. Expensive changes are applied immediately instead of being deferred to the next frame.
    /// </remarks>
    [Serializable]
    public class QualityLevelBinder : IntBinder
    {
        /// <param name="converter">
        /// An optional converter applied to the level before it is applied. Pass <see langword="null"/> to use the
        /// value unchanged. Runs in reverse only if it implements <see cref="ITwoWayConverter{TFrom, TTo}"/>.
        /// </param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/> — the value raises no change event to listen to.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public QualityLevelBinder(IConverter<int, int>? converter = null, BindMode mode = BindMode.OneWay)
            : base(converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }

        /// <inheritdoc/>
        protected sealed override int Property
        {
            get => QualitySettings.GetQualityLevel();
            set
            {
                var levels = QualitySettings.names.Length;
                var index = Mathf.Clamp(value, min: 0, max: levels - 1);

                QualitySettings.SetQualityLevel(index, applyExpensiveChanges: true);
            }
        }
    }
}
