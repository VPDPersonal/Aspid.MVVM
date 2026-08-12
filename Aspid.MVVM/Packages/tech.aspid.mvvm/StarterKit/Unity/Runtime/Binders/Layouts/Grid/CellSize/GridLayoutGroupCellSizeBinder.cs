#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetVector2Binder{GridLayoutGroup}"/> that binds <see cref="GridLayoutGroup.cellSize"/>.
    /// </summary>
    /// <remarks>
    /// The size of one cell of an inventory or level grid — what changes when the player picks a zoom level or
    /// the grid has to fit a different screen. The neighbouring layout groups had their spacing and padding
    /// bound and the grid had neither of its two numbers. Negative values are clamped to zero, which is also
    /// where a non-finite one lands.
    /// </remarks>
    [Serializable]
    public class GridLayoutGroupCellSizeBinder : TargetVector2Binder<GridLayoutGroup>
    {
        /// <inheritdoc/>
        protected sealed override Vector2 Property
        {
            get => Target.cellSize;
            set => Target.cellSize = new Vector2(BinderMath.SafeClamp(value.x, 0f, float.MaxValue), BinderMath.SafeClamp(value.y, 0f, float.MaxValue));
        }

        /// <inheritdoc/>
        public GridLayoutGroupCellSizeBinder(
            GridLayoutGroup target,
            IConverter<Vector2, Vector2>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
