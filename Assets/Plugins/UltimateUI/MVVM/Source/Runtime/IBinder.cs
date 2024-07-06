#nullable disable
using System;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM
{
    /// <summary>
    /// Represents a mechanism for binding and unbinding actions to values.
    /// </summary>
    public interface IBinder
    {
        /// <summary>
        /// Binds a value to an action that will be triggered when the value changes.
        /// </summary>
        /// <param name="value">The value to bind.</param>
        /// <param name="changed">The action to invoke when the value changes.</param>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <returns>True if the binding is successful; otherwise, false.</returns>
        public bool Bind<T>(in T value, ref Action<T> changed)
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
        
        /// <summary>
        /// Unbinds a previously bound action, stopping it from being triggered by value changes.
        /// </summary>
        /// <param name="changed">The action to unbind.</param>
        /// <typeparam name="T">The type of the value associated with the action.</typeparam>
        /// <returns>True if the unbinding is successful; otherwise, false.</returns>
        public bool Unbind<T>(ref Action<T> changed)
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
    
    /// <summary>
    /// Represents a type-specific mechanism for binding and setting values.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    public interface IBinder<in T> : IBinder
    {
        /// <summary>
        /// Sets the value to be bound and monitored for changes.
        /// </summary>
        /// <param name="value">The value to set.</param>
        public void SetValue(T value);
    }
}