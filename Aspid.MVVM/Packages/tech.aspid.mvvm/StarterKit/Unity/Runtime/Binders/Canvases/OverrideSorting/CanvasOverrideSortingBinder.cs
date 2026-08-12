#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBoolBinder{Canvas}"/> that binds <see cref="Canvas.overrideSorting"/>.
    /// </summary>
    /// <remarks>
    /// Whether this canvas sorts independently of its parent — the switch that makes the sorting order above take effect on a nested canvas.
    /// </remarks>
    [Serializable]
    public class CanvasOverrideSortingBinder : TargetBoolBinder<Canvas>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => Target.overrideSorting;
            set => Target.overrideSorting = value;
        }

        /// <inheritdoc/>
        public CanvasOverrideSortingBinder(
            Canvas target,
            bool isInvert = false,
            BindMode mode = BindMode.OneWay)
            : base(target, isInvert, mode) { }
    }
}
