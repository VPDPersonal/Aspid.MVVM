using UnityEngine;
using Aspid.MVVM.Unity;

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddComponentMenu("Aspid/MVVM/Binders/Behaviour/Behaviour Binder - Enabled")]
    [AddComponentContextMenu(typeof(Behaviour),"Add Behaviour Binder/Behaviour Binder - Enabled")]
    public partial class BehaviourEnabledMonoBinder : ComponentMonoBinder<Behaviour>, IBinder<bool>
    {
        [Header("Converter")]
        [SerializeField] private bool _isInvert;
        
        [BinderLog]
        public void SetValue(bool value) =>
            CachedComponent.enabled = _isInvert ? !value : value;
    }
}