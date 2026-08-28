#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="Binder{TProperty}">Binder&lt;bool&gt;</see> that binds <see cref="Screen.fullScreen"/>.
    /// </summary>
    /// <remarks>
    /// The windowed-or-fullscreen toggle every settings screen has. Unity applies the change at the end of the frame,
    /// so reading the property back immediately still reports the old state.
    /// </remarks>
    [Serializable]
    public class ScreenFullScreenBinder : Binder<bool>
    {
        /// <param name="converter">
        /// An optional converter applied to the value before it is applied. Pass <see langword="null"/> to use the
        /// value unchanged. Runs in reverse only if it implements <see cref="ITwoWayConverter{TFrom, TTo}"/>.
        /// </param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/> — the property raises no change event to listen to.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public ScreenFullScreenBinder(IConverter<bool, bool>? converter = null, BindMode mode = BindMode.OneWay)
            : base(converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }

        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => Screen.fullScreen;
            set => Screen.fullScreen = value;
        }
    }
}
