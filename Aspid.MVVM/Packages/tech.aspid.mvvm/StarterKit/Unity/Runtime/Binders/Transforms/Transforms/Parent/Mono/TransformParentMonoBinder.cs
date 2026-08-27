using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentObjectMonoBinder{T1, T2}">ComponentObjectMonoBinder&lt;Transform, Transform&gt;</see> that binds
    /// <see cref="Transform.parent"/>.
    /// </summary>
    /// <remarks>
    /// Reparenting is how an item moves from the world into a slot, from one slot to another, or back out — and
    /// nothing in the package could express it, so it lived in a MonoBehaviour written for the purpose.
    /// <para/>
    /// The transform keeps its local position and rotation, which is what a UI slot wants: the item lands where the
    /// slot is. A destroyed parent arrives as <see langword="null"/>, which detaches the object to the scene root
    /// rather than throwing.
    /// </remarks>
    [AddBinderContextMenu(typeof(Transform))]
    [AddComponentMenu("Aspid/MVVM/Binders/Transform/Transform Binder – Parent")]
    public class TransformParentMonoBinder : ComponentObjectMonoBinder<Transform, Transform>
    {
        /// <inheritdoc/>
        protected sealed override Transform Property
        {
            get => CachedComponent.parent;
            set => CachedComponent.SetParent(value, worldPositionStays: false);
        }
    }
}
