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
    /// <remarks>Negative and non-finite values are clamped to zero.</remarks>
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
