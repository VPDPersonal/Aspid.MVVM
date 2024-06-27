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
                binder.Bind(defaultValue, ref changed);
        }
        
        public static void Unbind<T>(ref Action<T> changed, IReadOnlyCollection<IBinder> binders)
        {
            foreach (var binder in binders)
                binder.Unbind(ref changed);
        }
        
        public static bool SetField<T>(ref T field, T newValue)
        {
            if (EqualityComparer<T>.Default.Equals(field, newValue)) return false;
            
            field = newValue;
            return true;
        }
        
        public static bool SetField<T>(ref T field, T newValue, IEqualityComparer<T> comparer)
        {
            if (comparer == null) throw new ArgumentNullException(nameof(comparer));
            if (comparer.Equals(field, newValue)) return false;
            
            field = newValue;
            return true;
        }
        
        public static bool SetField<T>(T oldValue, T newValue, Action<T> callback)
        {
            if (callback == null) throw new ArgumentNullException(nameof(callback));
            if (EqualityComparer<T>.Default.Equals(oldValue, newValue)) return false;
            
            callback(newValue);
            return true;
        }
        
        public static bool SetField<T>(T oldValue, T newValue, Action<T> callback, IEqualityComparer<T> comparer)
        {
            if (comparer == null) throw new ArgumentNullException(nameof(comparer));
            if (callback == null) throw new ArgumentNullException(nameof(callback));
            if (comparer.Equals(oldValue, newValue)) return false;
            
            callback(newValue);
            return true;
        }
    }
}