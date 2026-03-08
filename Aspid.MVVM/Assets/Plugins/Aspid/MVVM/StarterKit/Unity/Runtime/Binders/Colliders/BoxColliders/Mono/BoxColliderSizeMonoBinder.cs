using UnityEngine;


// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that sets the <see cref="BoxCollider.size"/> property on a <see cref="BoxCollider"/>
    /// when the bound ViewModel value changes.
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established the current value
    /// is sent back to the ViewModel.
    /// </summary>
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
