using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/Transform/Transform Binder – Euler Angles")]
    [AddBinderContextMenu(typeof(Transform), serializePropertyNames: "m_LocalRotation")]
    public partial class TransformEulerAnglesMonoBinder : ComponentVector3MonoBinder<Transform>
    {
        [SerializeField] private Space _space = Space.World;

        protected sealed override Vector3 Property
        {
            get => CachedComponent.GetEulerAngles(_space);
            set => CachedComponent.SetEulerAngles(value, _space);
        }
    }
}