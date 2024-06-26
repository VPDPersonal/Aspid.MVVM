using UnityEngine;
using UnityEngine.Events;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Events
{
    public class StringEventBinder : MonoBinder, IBinder<string>
    {
        public event UnityAction<string> StringValueSet
        {
            add => _stringValueSet.AddListener(value);
            remove => _stringValueSet.RemoveListener(value);
        }
        
        [Header("Events")]
        [SerializeField] private UnityEvent<string> _stringValueSet;
        
        public void SetValue(string value) =>
            _stringValueSet?.Invoke(value);
    }
}