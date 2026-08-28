#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetIntBinder{Renderer}"/> that binds <see cref="Renderer.sortingOrder"/>.
    /// </summary>
    [Serializable]
    public class RendererSortingOrderBinder : TargetIntBinder<Renderer>
    {
        /// <inheritdoc/>
        public RendererSortingOrderBinder(
            Renderer target,
            IConverter<int, int>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }

        /// <inheritdoc/>
        protected sealed override int Property
        {
            get => Target.sortingOrder;
            set => Target.sortingOrder = value;
        }
    }
}
