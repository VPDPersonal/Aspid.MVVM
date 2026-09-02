using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{Transform, Vector3}"/> that sets the <see cref="Transform.eulerAngles"/> or
    /// <see cref="Transform.localEulerAngles"/> property depending on the configured <see cref="Space"/>.
    /// </summary>
    [GenerateSerializableBinder]
    [AddComponentMenu("Aspid/MVVM/Binders/Transform/Transform Binder – Euler Angles")]
    [AddBinderContextMenu(typeof(Transform), serializePropertyNames: "m_LocalRotation")]
    public partial class TransformEulerAnglesMonoBinder : ComponentMonoBinder<Transform, Vector3>, IVector3Binder
    {
        [Tooltip("The coordinate space in which the euler angles are applied.")]
        [SerializeField] private Space _space = Space.World;

        protected sealed override Vector3 Property
        {
            get => CachedComponent.GetEulerAngles(_space);
            set => CachedComponent.SetEulerAngles(value, _space);
        }
    }
}