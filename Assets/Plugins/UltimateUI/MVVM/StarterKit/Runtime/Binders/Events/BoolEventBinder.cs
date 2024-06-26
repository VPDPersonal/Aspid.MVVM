using UnityEngine;
using UnityEngine.Events;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Events
{
    public class BoolEventBinder : MonoBinder, IBinder<bool>
    {
        public event UnityAction<bool> BoolValueSet
        {
            add => _boolValueSet.AddListener(value);
            remove => _boolValueSet.RemoveListener(value);
        }
        
        [Header("Events")]
        [SerializeField] private UnityEvent<bool> _boolValueSet;
        
        public void SetValue(bool value) =>
            _boolValueSet?.Invoke(value);
    }
}