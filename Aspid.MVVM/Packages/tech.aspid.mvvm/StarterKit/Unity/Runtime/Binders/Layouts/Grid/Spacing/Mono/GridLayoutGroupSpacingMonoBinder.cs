using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{GridLayoutGroup, Vector2}"/> that binds <see cref="GridLayoutGroup.spacing"/>.
    /// </summary>
    /// <remarks>
    /// Unlike <see cref="GridLayoutGroup.cellSize"/>, a negative value is meaningful and is not clamped —
    /// only a non-finite value is rejected.
    /// </remarks>
    [AddBinderContextMenu(typeof(GridLayoutGroup), serializePropertyNames: "m_Spacing")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/LayoutGroup/Grid/GridLayoutGroup Binder – Spacing")]
    public class GridLayoutGroupSpacingMonoBinder : ComponentMonoBinder<GridLayoutGroup, Vector2>, IVector2Binder
    {
        /// <inheritdoc/>
        protected sealed override Vector2 Property
        {
            get => CachedComponent.spacing;
            set
            {
                // A NaN component would put the whole layout at NaN, so only finite values are written.
                if (!this.RequireFinite(value)) return;
                CachedComponent.spacing = value;
            }
        }
    }
}
