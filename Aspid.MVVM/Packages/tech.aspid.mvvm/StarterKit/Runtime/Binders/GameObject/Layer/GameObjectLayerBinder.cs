#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{TTarget, TProperty}"/> that binds <see cref="GameObject.layer"/>.
    /// </summary>
    /// <remarks>
    /// An index that names no layer is reported and not written; children keep their layer.
    /// </remarks>
    [Serializable]
    public class GameObjectLayerBinder : TargetBinder<GameObject, int>
    {
        /// <param name="target">The object to bind.</param>
        /// <param name="converter">
        /// The converter applied to the bound value, or <see langword="null"/> to use it as-is.
        /// </param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="ArgumentException"><paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public GameObjectLayerBinder(
            GameObject target,
            IConverter<int, int>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }

        /// <inheritdoc/>
        protected sealed override int Property
        {
            get => Target.layer;
            set => Target.SetLayer(value, this);
        }
    }
}
