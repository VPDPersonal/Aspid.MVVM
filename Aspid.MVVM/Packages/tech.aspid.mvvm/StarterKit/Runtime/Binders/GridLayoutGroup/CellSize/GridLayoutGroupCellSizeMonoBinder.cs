using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent, TProperty}"/> that binds <see cref="GridLayoutGroup.cellSize"/>.
    /// </summary>
    /// <remarks>
    /// Negative and non-finite components become zero.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(GridLayoutGroup), serializePropertyNames: "m_CellSize")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/GridLayoutGroup/GridLayoutGroup Binder – Cell Size")]
    public class GridLayoutGroupCellSizeMonoBinder : ComponentMonoBinder<GridLayoutGroup, Vector2>, IVector2Binder
    {
        /// <inheritdoc/>
        protected sealed override Vector2 Property
        {
            get => CachedComponent.cellSize;
            set => CachedComponent.cellSize = this.NonNegative(value);
        }
    }
}
