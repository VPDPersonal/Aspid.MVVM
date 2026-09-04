using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent, TProperty}"/> that binds <see cref="GridLayoutGroup.spacing"/>.
    /// </summary>
    /// <remarks>
    /// Negative spacing is kept; a non-finite value is refused.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(GridLayoutGroup), serializePropertyNames: "m_Spacing")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/GridLayoutGroup/GridLayoutGroup Binder – Spacing")]
    public class GridLayoutGroupSpacingMonoBinder : ComponentMonoBinder<GridLayoutGroup, Vector2>, IVector2Binder
    {
        /// <inheritdoc/>
        protected sealed override Vector2 Property
        {
            get => CachedComponent.spacing;
            set
            {
                if (this.RequireFinite(value))
                    CachedComponent.spacing = value;
            }
        }
    }
}
