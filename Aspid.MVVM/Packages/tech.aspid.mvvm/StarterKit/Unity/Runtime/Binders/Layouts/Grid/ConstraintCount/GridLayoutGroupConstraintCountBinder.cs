#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetIntBinder{GridLayoutGroup}"/> that binds <see cref="GridLayoutGroup.constraintCount"/>.
    /// </summary>
    /// <remarks>
    /// How many columns or rows the grid is fixed to — the number an inventory changes when it is widened,
    /// and the number a responsive grid recomputes per screen. Meaningful only while
    /// <see cref="GridLayoutGroup.constraint"/> names an axis to count. Not clamped here: Unity raises
    /// anything below one to one itself.
    /// </remarks>
    [Serializable]
    public class GridLayoutGroupConstraintCountBinder : TargetIntBinder<GridLayoutGroup>
    {
        /// <inheritdoc/>
        protected sealed override int Property
        {
            get => Target.constraintCount;
            set => Target.constraintCount = value;
        }

        /// <inheritdoc/>
        public GridLayoutGroupConstraintCountBinder(
            GridLayoutGroup target,
            IConverter<int, int>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
