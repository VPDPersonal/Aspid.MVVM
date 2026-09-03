using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent, TProperty}"/> that binds <see cref="Transform.eulerAngles"/> or
    /// <see cref="Transform.localEulerAngles"/>.
    /// </summary>
    /// <remarks>
    /// Only a finite value is applied.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(Transform), serializePropertyNames: "m_LocalRotation")]
    [AddComponentMenu("Aspid/MVVM/Binders/Transform/Transform Binder – EulerAngles")]
    public class TransformEulerAnglesMonoBinder : ComponentMonoBinder<Transform, Vector3>, IVector3Binder
    {
        [Tooltip("Coordinate space the value is applied in.")]
        [SerializeField] private Space _space = Space.World;

        /// <inheritdoc/>
        protected sealed override Vector3 Property
        {
            get => CachedComponent.GetEulerAngles(_space);
            set => CachedComponent.SetEulerAngles(value, _space);
        }
    }
}
