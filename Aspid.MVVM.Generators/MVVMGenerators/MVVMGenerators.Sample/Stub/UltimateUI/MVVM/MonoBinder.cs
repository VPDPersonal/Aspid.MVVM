using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM
{
    public abstract class MonoBinder : MonoBehaviour, IBinder
    {
#if UNITY_EDITOR
        [field: SerializeField] 
        public string Id { get; set; }
#endif
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

        public virtual bool Unbind<T>(ref Action<T> changed)
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
    }
}