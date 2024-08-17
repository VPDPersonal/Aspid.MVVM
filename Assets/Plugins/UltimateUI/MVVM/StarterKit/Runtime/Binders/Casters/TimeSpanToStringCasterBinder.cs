using System;
using UnityEngine;
using UltimateUI.MVVM.Unity;
using UltimateUI.MVVM.Unity.Views;
using UltimateUI.MVVM.Unity.Generation;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Casters
{
    [AddComponentMenu("UI/Binders/Casters/TimeSpan To String Caster Binder")]
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