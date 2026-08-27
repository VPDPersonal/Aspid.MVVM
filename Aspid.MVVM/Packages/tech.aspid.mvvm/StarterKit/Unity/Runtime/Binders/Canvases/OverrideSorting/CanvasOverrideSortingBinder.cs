#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBoolBinder{Canvas}"/> that binds <see cref="Canvas.overrideSorting"/>.
    /// </summary>
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
