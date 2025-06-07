using UnityEngine;
using Aspid.MVVM.Unity;
using UnityEngine.Events;

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddPropertyContextMenu(typeof(bool))]
    [AddComponentMenu("Aspid/MVVM/Binders/UnityEvent/UnityEvent Binder - Bool")]
    [AddComponentContextMenu(typeof(Component),"Add UnityEvent Binder/UnityEvent Binder - Bool")]
    public sealed partial class UnityEventBoolMonoBinder : MonoBinder, IBinder<bool>
    {
        public event UnityAction<bool> Set
        {
            add => _set.AddListener(value);
            remove => _set.RemoveListener(value);
        }
        
        [Header("Converter")]
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