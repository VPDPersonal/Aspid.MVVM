#nullable disable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM
{
    public abstract class MonoBinder : MonoBehaviour, IBinder
    {
#if !ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED
        private static readonly Unity.Profiling.ProfilerMarker _bindMarker = new($"{nameof(MonoBinder)}.{nameof(Bind)}");
        private static readonly Unity.Profiling.ProfilerMarker _unbindMarker = new($"{nameof(MonoBinder)}.{nameof(Unbind)}");
#endif
        
#if UNITY_EDITOR
        [field: SerializeField] 
        public string Id { get; set; }
#endif
        
        bool IBinder.Bind<T>(in T value, ref Action<T> changed)
        {
#if !ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED
            using (_bindMarker.Auto()) 
#endif
            {
                return Bind(in value, ref changed);
            }
        }

        protected virtual bool Bind<T>(in T value, ref Action<T> changed)
        {
            switch (this)
            {
                case IBinder<T> specificBinder:
                    specificBinder.SetValue(value);
                    changed += specificBinder.SetValue;
                    return true;
                
                case IAnyBinder anyBinder:
                    anyBinder.SetValue(value);
                    changed += anyBinder.SetValue;
                    return true;
                
                default: return false;
            }
        }
        
        bool IBinder.Unbind<T>(ref Action<T> changed)
        {
#if !ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED
            using (_unbindMarker.Auto())
#endif
            {
                var result = Unbind(ref changed);
                if (result) ReleaseBinding<T>();
            
                return result;
            }
        }
        
        protected virtual bool Unbind<T>(ref Action<T> changed)
        {
            switch (this)
            {
                case IBinder<T> specificBinder:
                    changed -= specificBinder.SetValue;
                    return true;
                
                case IAnyBinder anyBinder:
                    changed -= anyBinder.SetValue;
                    return true;
                
                default: return false;
            }
        }

        protected virtual void ReleaseBinding<T>() { }
    }
}