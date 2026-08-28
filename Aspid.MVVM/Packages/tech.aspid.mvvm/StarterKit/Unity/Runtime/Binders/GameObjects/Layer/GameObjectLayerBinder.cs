#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{TTarget,TProperty}">TargetBinder&lt;GameObject, int&gt;</see>
    /// that binds the <see cref="GameObject.layer"/> property.
    /// </summary>
    /// <remarks>
    /// Only the object itself changes layer, not its children — the same as assigning the property by hand.
    /// An index that names no layer is logged as an error and written nowhere.
    /// </remarks>
    [Serializable]
    public class GameObjectLayerBinder : TargetBinder<GameObject, int>
    {
        private const int MaxLayer = 31;

        /// <param name="target">The <see cref="GameObject"/> whose layer is bound.</param>
        /// <param name="converter">
        /// An optional converter applied to the layer index before it is applied. Pass <see langword="null"/> to use
        /// the value unchanged. Runs in reverse only if it implements <see cref="ITwoWayConverter{TFrom, TTo}"/>.
        /// </param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/> — the layer raises no change event to listen to.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public GameObjectLayerBinder(GameObject target, IConverter<int, int>? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }

        /// <inheritdoc/>
        protected sealed override int Property
        {
            get => Target.layer;
            set
            {
                if (value is < 0 or > MaxLayer)
                {
                    this.LogError($"the layer {value} does not exist", "The layer is left unchanged.", Target);
                    return;
                }

                Target.layer = value;
            }
        }
    }
}
