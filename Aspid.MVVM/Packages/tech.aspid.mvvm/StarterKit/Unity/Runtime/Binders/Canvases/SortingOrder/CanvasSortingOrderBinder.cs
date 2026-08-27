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
        protected sealed override int Property
        {
            get => Target.sortingOrder;
            set => Target.sortingOrder = value;
        }

        /// <inheritdoc/>
        public CanvasSortingOrderBinder(
            Canvas target,
            IConverter<int, int>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
