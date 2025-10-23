using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Collider Binder - Enabled")]
    [AddComponentContextMenu(typeof(Collider),"Add Binder/Collider Binder - Enabled")]
    public partial class ColliderEnabledMonoBinder : ComponentMonoBinder<Collider>, IBinder<bool>
    {
        [SerializeField] private bool _isInvert;
        
        [BinderLog]
        public void SetValue(bool value) =>
            CachedComponent.enabled = _isInvert ? !value : value;
    }
}