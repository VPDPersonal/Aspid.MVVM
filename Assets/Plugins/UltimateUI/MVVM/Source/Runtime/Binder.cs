#nullable disable
using System;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM
{
    public abstract class Binder : IBinder
    {
        public virtual bool Bind<T>(in T value, ref Action<T> changed)
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
            var result = Unbind(ref changed);
            if (result) ReleaseBinding<T>();
            
            return result;
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