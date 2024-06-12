using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Casters
{
    public class TimeSpanToStringCasterBinder : MonoBinder, IBinder<TimeSpan>
    {
        // TODO Validate
        [Header("Binders")]
        [SerializeField] private MonoBinder[] _binders;
        
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