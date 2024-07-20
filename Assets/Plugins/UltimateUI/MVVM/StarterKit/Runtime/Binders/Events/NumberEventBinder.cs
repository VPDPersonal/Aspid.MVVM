using UnityEngine;
using UnityEngine.Events;
using UltimateUI.MVVM.Unity;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Events
{
    [AddComponentMenu("UI/Binders/Event/Event Binder - Number")]
    public partial class NumberEventBinder : MonoBinder, IBinderNumber
    {
        public event UnityAction<int> IntValueSet
        {
            add => _intValueSet.AddListener(value);
            remove => _intValueSet.RemoveListener(value);
        }
        
        public event UnityAction<long> LongValueSet
        {
            add => _longValueSet.AddListener(value);
            remove => _longValueSet.RemoveListener(value);
        }
        
        public event UnityAction<float> FloatValueSet
        {
            add => _floatValueSet.AddListener(value);
            remove => _floatValueSet.RemoveListener(value);
        }
        
        public event UnityAction<double> DoubleValueSet
        {
            add => _doubleValueSet.AddListener(value);
            remove => _doubleValueSet.RemoveListener(value);
        }
        
        [Header("Events")]
        [SerializeField] private UnityEvent<int> _intValueSet;
        [SerializeField] private UnityEvent<long> _longValueSet;
        [SerializeField] private UnityEvent<float> _floatValueSet;
        [SerializeField] private UnityEvent<double> _doubleValueSet;
        
        [BinderLog]
        public void SetValue(int value) =>
            _intValueSet?.Invoke(value);
        
        [BinderLog]
        public void SetValue(long value) =>
            _longValueSet?.Invoke(value);
        
        [BinderLog]
        public void SetValue(float value) =>
            _floatValueSet?.Invoke(value);
        
        [BinderLog]
        public void SetValue(double value) =>
            _doubleValueSet?.Invoke(value);
    }
}