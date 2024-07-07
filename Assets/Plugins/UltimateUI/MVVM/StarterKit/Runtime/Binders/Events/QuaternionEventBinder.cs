using UnityEngine;
using UnityEngine.Events;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Events
{
    public partial class QuaternionEventBinder : MonoBinder, IBinder<Quaternion>
    {
        public event UnityAction<Quaternion> QuaternionValueSet
        {
            add => _quaternionValueSet.AddListener(value);
            remove => _quaternionValueSet.RemoveListener(value);
        }
        
        [Header("Events")]
        [SerializeField] private UnityEvent<Quaternion> _quaternionValueSet;
        
        [BinderLog]
        public void SetValue(Quaternion value) =>
            _quaternionValueSet?.Invoke(value);
    }
}