using UnityEngine;
using UnityEngine.Events;
using UltimateUI.MVVM.Unity;
using UltimateUI.MVVM.Unity.Generation;

namespace UltimateUI.MVVM.StarterKit.Binders.Unity.UnityEvents
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