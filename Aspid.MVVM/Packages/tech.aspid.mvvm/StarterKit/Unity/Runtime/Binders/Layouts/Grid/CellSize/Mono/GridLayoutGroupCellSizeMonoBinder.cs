using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentVector2MonoBinder{GridLayoutGroup}"/> that binds <see cref="GridLayoutGroup.cellSize"/>.
    /// </summary>
    /// <remarks>
    /// The size of one cell of an inventory or level grid — what changes when the player picks a zoom level or
    /// the grid has to fit a different screen. The neighbouring layout groups had their spacing and padding
    /// bound and the grid had neither of its two numbers. Negative values are clamped to zero, which is also
    /// where a non-finite one lands.
    /// </remarks>
    [AddBinderContextMenu(typeof(GridLayoutGroup), serializePropertyNames: "m_CellSize")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/LayoutGroup/Grid/GridLayoutGroup Binder – Cell Size")]
    public class GridLayoutGroupCellSizeMonoBinder : ComponentVector2MonoBinder<GridLayoutGroup>
    {
        /// <inheritdoc/>
        protected sealed override Vector2 Property
        {
            get => CachedComponent.cellSize;
            set => CachedComponent.cellSize = new Vector2(BinderMath.SafeClamp(value.x, 0f, float.MaxValue), BinderMath.SafeClamp(value.y, 0f, float.MaxValue));
        }
    }
}
