using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentObjectMonoBinder{T1, T2}">ComponentObjectMonoBinder&lt;Transform, Transform&gt;</see> that binds
    /// <see cref="Transform.parent"/>.
    /// </summary>
    /// <remarks>
    /// The transform keeps its local position and rotation. A destroyed parent arrives as
    /// <see langword="null"/>, which detaches the object to the scene root rather than throwing.
    /// </remarks>
    [GenerateSerializableBinder]
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
