#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetIntBinder{Renderer}"/> that binds <see cref="Renderer.sortingOrder"/>.
    /// </summary>
    /// <remarks>
    /// Draw order within a sorting layer, for every renderer and not only a sprite one: the package bound
    /// <see cref="SpriteRenderer"/>'s order and left the property it inherits from
    /// <see cref="Renderer"/> unbound, so a line, a mesh or a trail could not be brought to the front.
    /// </remarks>
    [Serializable]
    public class RendererSortingOrderBinder : TargetIntBinder<Renderer>
    {
        /// <inheritdoc/>
        protected sealed override int Property
        {
            get => Target.sortingOrder;
            set => Target.sortingOrder = value;
        }

        /// <inheritdoc/>
        public RendererSortingOrderBinder(
            Renderer target,
            IConverter<int, int>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
