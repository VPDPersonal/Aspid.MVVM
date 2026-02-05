using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Collider Binder – IsTrigger")]
    [AddBinderContextMenu(typeof(Collider), serializePropertyNames: "m_IsTrigger")]
    public partial class ColliderIsTriggerMonoBinder : ComponentMonoBinder<Collider>, IBinder<bool>
    {
        [SerializeField] private bool _isInvert;
        
        [BinderLog]
        public void SetValue(bool value) =>
            CachedComponent.isTrigger = _isInvert ? !value : value;
    }
}