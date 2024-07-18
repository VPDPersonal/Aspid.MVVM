using System;
using UnityEngine;
using UltimateUI.MVVM.Unity;
using UltimateUI.MVVM.Unity.Views;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Casters
{
    public partial class TimeSpanToStringCasterBinder : MonoBinder, IBinder<TimeSpan>
    {
        [Header("Binders")]
        [RequireBinder(typeof(string))]
        [SerializeField] private MonoBinder[] _binders;
        
#if !ULTIMATE_UI_MVVM_STARTER_KIT_BINDER_LOG_GENERATOR_DISABLED
        [BinderLog]
#endif
        public void SetValue(TimeSpan value)
        {
            var casterValue = value.ToString();
            
            foreach (var binder in _binders)
            {
                if (binder is IBinder<string> stringBinder)
                    stringBinder.SetValue(casterValue);
            }
        }
    }
}