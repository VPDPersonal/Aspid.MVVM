#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{Canvas, bool}"/> that binds <see cref="Canvas.overrideSorting"/>.
    /// </summary>
    [Serializable]
    public class CanvasOverrideSortingBinder : TargetBinder<Canvas, bool>
    {
        /// <inheritdoc/>
        public CanvasOverrideSortingBinder(
            Canvas target,
            IConverter<bool, bool>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }

        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => Target.overrideSorting;
            set => Target.overrideSorting = value;
        }
    }
}
