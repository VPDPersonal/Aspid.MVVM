using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentVector2MonoBinder{GridLayoutGroup}"/> that binds <see cref="GridLayoutGroup.cellSize"/>.
    /// </summary>
    /// <remarks>Negative and non-finite values are clamped to zero.</remarks>
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
