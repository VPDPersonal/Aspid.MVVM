#nullable disable
using System;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM
{
    public abstract class Binder : IBinder
    {
#if UNITY_EDITOR
        private static readonly Unity.Profiling.ProfilerMarker _bindMarker = new($"{nameof(Binder)}.{nameof(Bind)}");
        private static readonly Unity.Profiling.ProfilerMarker _unbindMarker = new($"{nameof(Binder)}.{nameof(Unbind)}");
#endif
        
        bool IBinder.Bind<T>(in T value, ref Action<T> changed)
        {
#if UNITY_EDITOR
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
#if UNITY_EDITOR
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