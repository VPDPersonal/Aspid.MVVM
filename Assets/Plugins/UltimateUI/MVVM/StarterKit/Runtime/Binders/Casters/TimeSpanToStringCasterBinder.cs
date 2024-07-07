using System;
using UnityEngine;
using UltimateUI.MVVM.Views;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Casters
{
    public partial class TimeSpanToStringCasterBinder : MonoBinder, IBinder<TimeSpan>
    {
        [Header("Binders")]
        [RequireBinder(typeof(string))]
        [SerializeField] private MonoBinder[] _binders;
        
        [BinderLog]
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