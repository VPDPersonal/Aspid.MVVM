using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent,TProperty}">ComponentMonoBinder&lt;GridLayoutGroup, GridLayoutGroup.Constraint&gt;</see> that binds
    /// <see cref="GridLayoutGroup.constraint"/>.
    /// </summary>
    /// <remarks>
    /// Paired with <see cref="GridLayoutGroupConstraintCountMonoBinder"/> — the count means nothing until this
    /// names which axis it counts.
    /// </remarks>
    [AddBinderContextMenu(typeof(GridLayoutGroup), serializePropertyNames: "m_Constraint")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/LayoutGroup/Grid/GridLayoutGroup Binder – Constraint")]
    public class GridLayoutGroupConstraintMonoBinder : ComponentMonoBinder<GridLayoutGroup, GridLayoutGroup.Constraint>
    {
        /// <inheritdoc/>
        protected sealed override GridLayoutGroup.Constraint Property
        {
            get => CachedComponent.constraint;
            set => CachedComponent.constraint = value;
        }
    }
}
