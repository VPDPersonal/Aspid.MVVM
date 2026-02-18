using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/Transform/Transform Binder – Position")]
    [AddBinderContextMenu(typeof(Transform), serializePropertyNames: "m_LocalPosition")]
    public class TransformPositionMonoBinder : ComponentVector3MonoBinder<Transform>
    {
        [SerializeField] private Space _space = Space.World;

        protected sealed override Vector3 Property
        {
            get => CachedComponent.GetPosition(_space);
            set => CachedComponent.SetPosition(value, _space);
        }
    }
}