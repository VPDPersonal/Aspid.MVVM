#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetVector2Binder{GridLayoutGroup}"/> that binds <see cref="GridLayoutGroup.spacing"/>.
    /// </summary>
    /// <remarks>
    /// Unlike <see cref="GridLayoutGroup.cellSize"/>, a negative value is meaningful and is not clamped —
    /// only a non-finite value is rejected.
    /// </remarks>
    [Serializable]
    public class GridLayoutGroupSpacingBinder : TargetVector2Binder<GridLayoutGroup>
    {
        /// <inheritdoc/>
        protected sealed override Vector2 Property
        {
            get => Target.spacing;
            set
            {
                // A NaN component would put the whole layout at NaN, so only finite values are written.
                if (!BinderMath.IsFinite(value.x) || !BinderMath.IsFinite(value.y)) return;
                Target.spacing = value;
            }
        }

        /// <inheritdoc/>
        public GridLayoutGroupSpacingBinder(
            GridLayoutGroup target,
            IConverter<Vector2, Vector2>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
