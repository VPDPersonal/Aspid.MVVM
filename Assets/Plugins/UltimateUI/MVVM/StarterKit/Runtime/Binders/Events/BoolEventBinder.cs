using UnityEngine;
using UnityEngine.Events;
using UltimateUI.MVVM.Unity;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Events
{
    [AddComponentMenu("UI/Binders/Event/Event Binder - Bool")]
    public partial class BoolEventBinder : MonoBinder, IBinder<bool>
    {
        public event UnityAction<bool> BoolValueSet
        {
            add => _boolValueSet.AddListener(value);
            remove => _boolValueSet.RemoveListener(value);
        }
        
        [Header("Events")]
        [SerializeField] private UnityEvent<bool> _boolValueSet;
        
        [BinderLog]
        public void SetValue(bool value) =>
            _boolValueSet?.Invoke(value);
    }
}