using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentIntMonoBinder{TComponent}"/> that binds <see cref="GridLayoutGroup.constraintCount"/>.
    /// </summary>
    /// <remarks>
    /// Applies only while <see cref="GridLayoutGroup.constraint"/> names an axis; Unity raises values below one to one.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(GridLayoutGroup), serializePropertyNames: "m_ConstraintCount")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/GridLayoutGroup/GridLayoutGroup Binder – Constraint Count")]
    public class GridLayoutGroupConstraintCountMonoBinder : ComponentIntMonoBinder<GridLayoutGroup>
    {
        /// <inheritdoc/>
        protected sealed override int Property
        {
            get => CachedComponent.constraintCount;
            set => CachedComponent.constraintCount = value;
        }
    }
}
