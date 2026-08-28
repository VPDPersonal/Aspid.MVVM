#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{GridLayoutGroup, Vector2}"/> that binds <see cref="GridLayoutGroup.cellSize"/>.
    /// </summary>
    /// <remarks>Negative and non-finite values are clamped to zero.</remarks>
    [Serializable]
    public class GridLayoutGroupCellSizeBinder : TargetBinder<GridLayoutGroup, Vector2>, IVector2Binder
    {
        /// <inheritdoc/>
        protected sealed override Vector2 Property
        {
            get => Target.cellSize;
            set => Target.cellSize = new Vector2(this.SafeClamp(value.x, 0f, float.MaxValue, Target), this.SafeClamp(value.y, 0f, float.MaxValue, Target));
        }

        /// <inheritdoc/>
        public GridLayoutGroupCellSizeBinder(
            GridLayoutGroup target,
            IConverter<Vector2, Vector2>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
