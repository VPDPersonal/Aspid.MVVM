using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent, TProperty}"/> that binds <see cref="Transform.position"/> or
    /// <see cref="Transform.localPosition"/>.
    /// </summary>
    /// <remarks>
    /// Only a finite value is applied.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(Transform), serializePropertyNames: "m_LocalPosition")]
    [AddComponentMenu("Aspid/MVVM/Binders/Transform/Transform Binder – Position")]
    public class TransformPositionMonoBinder : ComponentMonoBinder<Transform, Vector3>, IVector3Binder
    {
        [Tooltip("Coordinate space the value is applied in.")]
        [SerializeField] private Space _space = Space.World;

        /// <inheritdoc/>
        protected sealed override Vector3 Property
        {
            get => CachedComponent.GetPosition(_space);
            set => CachedComponent.SetPosition(value, _space);
        }
    }
}
