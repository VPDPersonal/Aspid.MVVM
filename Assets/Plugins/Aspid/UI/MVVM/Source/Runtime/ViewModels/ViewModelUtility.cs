using System;
using System.Collections.Generic;

namespace Aspid.UI.MVVM.ViewModels
{
    public static class ViewModelUtility
    {
        public static bool SetProperty<T>(ref T field, T newValue)
        {
            if (EqualsDefault(field, newValue)) return false;
            
            field = newValue;
            return true;
        }
        
        public static bool SetProperty<T>(ref T field, T newValue, IEqualityComparer<T> comparer)
        {
            if (comparer == null) throw new ArgumentNullException(nameof(comparer));
            if (comparer.Equals(field, newValue)) return false;
            
            field = newValue;
            return true;
        }
        
        public static bool SetProperty<T>(T oldValue, T newValue, Action<T> callback)
        {
            if (callback == null) throw new ArgumentNullException(nameof(callback));
            if (EqualsDefault(oldValue, newValue)) return false;
            
            callback(newValue);
            return true;
        }
        
        public static bool SetProperty<T>(T oldValue, T newValue, Action<T> callback, IEqualityComparer<T> comparer)
        {
            if (comparer == null) throw new ArgumentNullException(nameof(comparer));
            if (callback == null) throw new ArgumentNullException(nameof(callback));
            if (comparer.Equals(oldValue, newValue)) return false;
            
            callback(newValue);
            return true;
        }
        
        public static bool EqualsDefault<T>(T field, T newValue) =>
            EqualityComparer<T>.Default.Equals(field, newValue);
    }
}