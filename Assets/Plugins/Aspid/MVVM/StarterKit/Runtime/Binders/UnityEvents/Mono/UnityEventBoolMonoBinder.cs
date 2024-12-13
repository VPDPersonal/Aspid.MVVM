using UnityEngine;
using Aspid.MVVM.Mono;
using UnityEngine.Events;
using Aspid.MVVM.Mono.Generation;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Binders/UnityEvent/UnityEvent Binder - Bool")]
    public sealed partial class UnityEventBoolMonoBinder : MonoBinder, IBinder<bool>
    {
        public event UnityAction<bool> Set
        {
            add => _set.AddListener(value);
            remove => _set.RemoveListener(value);
        }
        
        [Header("Parameter")]
        [SerializeField] private bool _isInvert;
        
        [Header("Events")]
        [SerializeField] private UnityEvent<bool> _set;
        
        [BinderLog]
        public void SetValue(bool value)
        {
            value = _isInvert ? !value : value;
            _set?.Invoke(value);
        }
    }
}