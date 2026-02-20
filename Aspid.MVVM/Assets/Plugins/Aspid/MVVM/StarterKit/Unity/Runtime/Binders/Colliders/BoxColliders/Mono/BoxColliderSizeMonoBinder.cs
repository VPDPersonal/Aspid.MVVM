using UnityEngine;


// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(BoxCollider), serializePropertyNames: "m_Size")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Box/BoxCollider Binder – Size")]
    public class BoxColliderSizeMonoBinder : ComponentVector3MonoBinder<BoxCollider>
    {
        protected sealed override Vector3 Property
        {
            get => CachedComponent.size;
            set => CachedComponent.size = value; 
        }
    }
}