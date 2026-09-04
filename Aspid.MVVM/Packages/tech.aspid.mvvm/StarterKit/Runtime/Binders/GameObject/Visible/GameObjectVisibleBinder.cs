#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{TTarget, TProperty}"/> that shows or hides the object via
    /// <see cref="GameObject.SetActive"/>.
    /// </summary>
    [Serializable]
    public sealed class GameObjectVisibleBinder : TargetBinder<GameObject, bool>
    {
        /// <param name="target">The object to bind.</param>
        /// <param name="converter">
        /// The converter applied to the bound value, or <see langword="null"/> to use it as-is.
        /// </param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="ArgumentException"><paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public GameObjectVisibleBinder(
            GameObject target,
            IConverter<bool, bool>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }

        /// <inheritdoc/>
        protected override bool Property
        {
            get => Target.activeSelf;
            set => Target.SetActive(value);
        }
    }
}
