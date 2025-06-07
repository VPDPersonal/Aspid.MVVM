using UnityEngine;
using Aspid.MVVM.Unity;

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddPropertyContextMenu(typeof(Collider), "m_IsTrigger")]   
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Collider Binder - IsTrigger")]
    [AddComponentContextMenu(typeof(Collider),"Add Binder/Collider Binder - IsTrigger")]
    public partial class ColliderIsTriggerMonoBinder : ComponentMonoBinder<Collider>, IBinder<bool>
    {
        [Header("Converter")]
        [SerializeField] private bool _isInvert;
        
        [BinderLog]
        public void SetValue(bool value) =>
            CachedComponent.isTrigger = _isInvert ? !value : value;
    }
}