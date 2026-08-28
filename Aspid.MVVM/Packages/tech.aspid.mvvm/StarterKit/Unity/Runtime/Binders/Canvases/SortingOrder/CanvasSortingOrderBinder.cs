#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetIntBinder{Canvas}"/> that binds <see cref="Canvas.sortingOrder"/>.
    /// </summary>
    [Serializable]
    public class CanvasSortingOrderBinder : TargetIntBinder<Canvas>
    {
        /// <inheritdoc/>
        public CanvasSortingOrderBinder(
            Canvas target,
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
