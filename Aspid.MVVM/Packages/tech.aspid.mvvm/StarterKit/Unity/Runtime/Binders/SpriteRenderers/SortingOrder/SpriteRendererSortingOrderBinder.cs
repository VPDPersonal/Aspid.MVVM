#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetIntBinder{SpriteRenderer}"/> that binds <see cref="SpriteRenderer.sortingOrder"/>.
    /// </summary>
    /// <remarks>
    /// Draw order inside a sorting layer — the usual way a 2D scene expresses depth.
    /// </remarks>
    [Serializable]
    public class SpriteRendererSortingOrderBinder : TargetIntBinder<SpriteRenderer>
    {
        /// <inheritdoc/>
        protected sealed override int Property
        {
            get => Target.sortingOrder;
            set => Target.sortingOrder = value;
        }

        /// <inheritdoc/>
        public SpriteRendererSortingOrderBinder(
            SpriteRenderer target,
            IConverter<int, int>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
