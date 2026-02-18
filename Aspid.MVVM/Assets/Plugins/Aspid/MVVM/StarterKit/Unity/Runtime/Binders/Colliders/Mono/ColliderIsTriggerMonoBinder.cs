using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Collider Binder – IsTrigger")]
    [AddBinderContextMenu(typeof(Collider), serializePropertyNames: "m_IsTrigger")]
    public class ColliderIsTriggerMonoBinder : ComponentBoolMonoBinder<Collider>
    {
        protected sealed override bool Property
        {
            get => CachedComponent.isTrigger;
            set => CachedComponent.isTrigger = value;
        }
    }
}