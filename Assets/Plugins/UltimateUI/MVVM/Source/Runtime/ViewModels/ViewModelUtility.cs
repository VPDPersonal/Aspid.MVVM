using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.ViewModels
{
    public static class ViewModelUtility
    {
#if UNITY_EDITOR && !ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED
        private static readonly Unity.Profiling.ProfilerMarker _bindMarker = new($"{nameof(ViewModelUtility)}.{nameof(Bind)}");
        private static readonly Unity.Profiling.ProfilerMarker _unbindMarker = new($"{nameof(ViewModelUtility)}.{nameof(Unbind)}");
#endif
        
        public static void Bind<T>(T defaultValue, ref Action<T> changed, IReadOnlyCollection<IBinder> binders)
        {
#if UNITY_EDITOR && !ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED
            using (_bindMarker.Auto())
#endif
            {
                foreach (var binder in binders)
                    binder.Bind(defaultValue, ref changed);
            }
        }
        
        public static void Unbind<T>(ref Action<T> changed, IReadOnlyCollection<IBinder> binders)
        {
#if UNITY_EDITOR && !ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED
            using (_unbindMarker.Auto())
#endif
            {
                foreach (var binder in binders)
                    binder.Unbind(ref changed);
            }
        }
        
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