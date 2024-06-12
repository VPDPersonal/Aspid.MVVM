using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.ViewModels
{
    public static class ViewModelUtility
    {
        public static void Bind<T>(T defaultValue, ref Action<T> changed, IReadOnlyCollection<IBinder> binders)
        {
            foreach (var binder in binders)
            {
                switch (binder)
                {
                    case IBinder<T> specificBinder:
                        specificBinder.SetValue(defaultValue);
                        changed += specificBinder.SetValue;
                        break;
                    
                    case IAnyBinder anyBinder:
                        anyBinder.SetValue(defaultValue);
                        changed += anyBinder.SetValue;
                        break;
                }
            }
        }
        
        public static void Unbind<T>(ref Action<T> changed, IReadOnlyCollection<IBinder> binders)
        {
            foreach (var binder in binders)
            {
                switch (binder)
                {
                    case IBinder<T> specificBinder: changed -= specificBinder.SetValue; break;
                    case IAnyBinder anyBinder: changed -= anyBinder.SetValue; break;
                }
            }
        }
        
        public static void SetValue<T>(ref T value, T newValue, Action<T> changed)
        {
            if (value.Equals(newValue)) return;
            
            value = newValue;
            changed?.Invoke(value);
        }
    }
}