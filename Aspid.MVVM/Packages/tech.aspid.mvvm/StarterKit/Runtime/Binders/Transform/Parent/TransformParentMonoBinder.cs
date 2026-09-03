using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentObjectMonoBinder{TComponent, TObject}"/> that binds <see cref="Transform.parent"/>.
    /// </summary>
    /// <remarks>
    /// Local position and rotation are kept; <see langword="null"/> detaches the object to the scene root.
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
