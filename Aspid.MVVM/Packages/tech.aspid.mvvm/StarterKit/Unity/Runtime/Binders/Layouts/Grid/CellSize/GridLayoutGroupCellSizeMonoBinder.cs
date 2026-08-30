using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{GridLayoutGroup, Vector2}"/> that binds <see cref="GridLayoutGroup.cellSize"/>.
    /// </summary>
    /// <remarks>Negative and non-finite values are clamped to zero.</remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(GridLayoutGroup), serializePropertyNames: "m_CellSize")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/LayoutGroup/Grid/GridLayoutGroup Binder – Cell Size")]
    public class GridLayoutGroupCellSizeMonoBinder : ComponentMonoBinder<GridLayoutGroup, Vector2>, IVector2Binder
    {
        /// <inheritdoc/>
        protected sealed override Vector2 Property
        {
            get => CachedComponent.cellSize;
            set => CachedComponent.cellSize = new Vector2(this.SafeClamp(value.x, 0f, float.MaxValue), this.SafeClamp(value.y, 0f, float.MaxValue));
        }
    }
}
