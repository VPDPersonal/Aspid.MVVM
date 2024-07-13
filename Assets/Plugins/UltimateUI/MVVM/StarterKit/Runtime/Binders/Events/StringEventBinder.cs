using UnityEngine;
using UnityEngine.Events;
using System.Globalization;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Events
{
    public partial class StringEventBinder : MonoBinder, IBinder<string>, IBinderNumber
    {
        public event UnityAction<string> StringValueSet
        {
            add => _stringValueSet.AddListener(value);
            remove => _stringValueSet.RemoveListener(value);
        }
        
        [Header("Events")]
        [SerializeField] private UnityEvent<string> _stringValueSet;
        
#if !ULTIMATE_UI_MVVM_STARTER_KIT_BINDER_LOG_GENERATOR_DISABLED
        [BinderLog]
#endif
        public void SetValue(string value) =>
            _stringValueSet?.Invoke(value);

#if !ULTIMATE_UI_MVVM_STARTER_KIT_BINDER_LOG_GENERATOR_DISABLED
        [BinderLog]
#endif
        public void SetValue(int value) =>
            SetValue(value.ToString());
                
#if !ULTIMATE_UI_MVVM_STARTER_KIT_BINDER_LOG_GENERATOR_DISABLED
        [BinderLog]
#endif
        public void SetValue(long value) =>
            SetValue(value.ToString());
        
#if !ULTIMATE_UI_MVVM_STARTER_KIT_BINDER_LOG_GENERATOR_DISABLED
        [BinderLog]
#endif
        public void SetValue(float value) =>
            SetValue(value.ToString(CultureInfo.InvariantCulture));
                
#if !ULTIMATE_UI_MVVM_STARTER_KIT_BINDER_LOG_GENERATOR_DISABLED
        [BinderLog]
#endif
        public void SetValue(double value) =>
            SetValue(value.ToString(CultureInfo.InvariantCulture));
    }
}