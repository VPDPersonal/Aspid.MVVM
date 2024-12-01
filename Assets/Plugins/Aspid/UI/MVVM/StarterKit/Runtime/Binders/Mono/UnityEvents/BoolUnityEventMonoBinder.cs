using UnityEngine;
using UnityEngine.Events;
using Aspid.UI.MVVM.Mono;
using Aspid.UI.MVVM.Mono.Generation;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("UI/Binders/UnityEvent/UnityEvent Binder - Bool")]
    public sealed partial class BoolUnityEventMonoBinder : MonoBinder, IBinder<bool>
    {
        public event UnityAction<bool> BoolValueSet
        {
            add => _boolValueSet.AddListener(value);
            remove => _boolValueSet.RemoveListener(value);
        }

        [Header("Parameter")]
        [SerializeField] private bool _isInvert;
        
        [Header("Events")]
        [SerializeField] private UnityEvent<bool> _boolValueSet;
        
        [BinderLog]
        public void SetValue(bool value)
        {
            value = _isInvert ? !value : value;
            _boolValueSet?.Invoke(value);
        }
    }
}