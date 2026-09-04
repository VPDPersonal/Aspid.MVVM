using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent, TProperty}"/> that binds <see cref="GridLayoutGroup.constraint"/>.
    /// </summary>
    /// <remarks>
    /// Pairs with <see cref="GridLayoutGroupConstraintCountMonoBinder"/>.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(GridLayoutGroup), serializePropertyNames: "m_Constraint")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/GridLayoutGroup/GridLayoutGroup Binder – Constraint")]
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
