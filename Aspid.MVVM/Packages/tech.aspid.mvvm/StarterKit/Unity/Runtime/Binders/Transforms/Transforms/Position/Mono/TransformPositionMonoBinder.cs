using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{Transform, Vector3}"/> that sets the <see cref="Transform.position"/> or
    /// <see cref="Transform.localPosition"/> property depending on the configured <see cref="Space"/>.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Transform/Transform Binder – Position")]
    [AddBinderContextMenu(typeof(Transform), serializePropertyNames: "m_LocalPosition")]
    public class TransformPositionMonoBinder : ComponentMonoBinder<Transform, Vector3>, IVector3Binder
    {
        [Tooltip("The coordinate space in which the position is applied.")]
        [SerializeField] private Space _space = Space.World;

        protected sealed override Vector3 Property
        {
            get => CachedComponent.GetPosition(_space);
            set => CachedComponent.SetPosition(value, _space);
        }
    }
}