using System;
using System.Collections.Generic;

namespace Aspid.MVVM.ViewModels
{
    /// <summary>
    /// Utility class providing common operations for working with ViewModels.
    /// </summary>
    public static class ViewModelUtility
    {
        /// <summary>
        /// Sets the value of a property if it has changed.
        /// </summary>
        /// <param name="field">Reference to the field storing the current value.</param>
        /// <param name="newValue">New value to set.</param>
        /// <typeparam name="T">Property type.</typeparam>
        /// <returns>Returns <c>true</c> if the property was changed; otherwise, <c>false</c>.</returns>
        public static bool SetProperty<T>(ref T field, T newValue)
        {
            if (EqualsDefault(field, newValue)) return false;
            
            field = newValue;
            return true;
        }
        
        /// <summary>
        /// Sets the value of a property using a custom <see cref="comparer"/> if it has changed.
        /// </summary>
        /// <param name="field">Reference to the field storing the current value.</param>
        /// <param name="newValue">New value to set.</param>
        /// <param name="comparer">Custom <see cref="comparer"/> for equality checking.</param>
        /// <typeparam name="T">Property type.</typeparam>
        /// <returns>Returns <c>true</c> if the property was changed; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static bool SetProperty<T>(ref T field, T newValue, IEqualityComparer<T> comparer)
        {
            if (comparer == null) throw new ArgumentNullException(nameof(comparer));
            if (comparer.Equals(field, newValue)) return false;
            
            field = newValue;
            return true;
        }
        
        /// <summary>
        /// Sets the value of a property and invokes <see cref="callback"/> if it has changed.
        /// </summary>
        /// <param name="oldValue">Old value.</param>
        /// <param name="newValue">New value to set.</param>
        /// <param name="callback">Action to invoke if the value was changed.</param>
        /// <typeparam name="T">Property type.</typeparam>
        /// <returns>Returns <c>true</c> if the property was changed; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static bool SetProperty<T>(T oldValue, T newValue, Action<T> callback)
        {
            if (callback == null) throw new ArgumentNullException(nameof(callback));
            if (EqualsDefault(oldValue, newValue)) return false;
            
            callback(newValue);
            return true;
        }
        
        /// <summary>
        /// Sets the value of a property using a custom <see cref="comparer"/> and invokes <see cref="callback"/> if it has changed.
        /// </summary>
        /// <param name="oldValue">Old value.</param>
        /// <param name="newValue">New value to set.</param>
        /// <param name="callback">Action to invoke if the value was changed.</param>
        /// <param name="comparer">Custom <see cref="comparer"/> for equality checking.</param>
        /// <typeparam name="T">Property type.</typeparam>
        /// <returns>Returns <c>true</c> if the property was changed; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static bool SetProperty<T>(T oldValue, T newValue, Action<T> callback, IEqualityComparer<T> comparer)
        {
            if (comparer == null) throw new ArgumentNullException(nameof(comparer));
            if (callback == null) throw new ArgumentNullException(nameof(callback));
            if (comparer.Equals(oldValue, newValue)) return false;
            
            callback(newValue);
            return true;
        }
        
        /// <summary>
        /// Checks equality of values using the default Comparer.
        /// </summary>
        /// <param name="value">Current value.</param>
        /// <param name="newValue">New value of the field.</param>
        /// <typeparam name="T">Value type.</typeparam>
        /// <returns>Returns <c>true</c> if the values are equal, otherwise <c>false</c>.</returns>
        public static bool EqualsDefault<T>(T value, T newValue) =>
            EqualityComparer<T>.Default.Equals(value, newValue);
        
        /// <summary>
        /// Adds a binder to the specified ViewModel event for a given value.
        /// </summary>
        /// <typeparam name="T">The type of the value associated with the binder and event.</typeparam>
        /// <param name="binder">The binder to be added, which will manage the binding logic.</param>
        /// <param name="value">The value to be bound to the ViewModel event.</param>
        /// <param name="viewModelEvent">
        /// A reference to the ViewModel event for the specified type <typeparamref name="T"/>.
        /// If the event is null, a new instance will be created.
        /// </param>
        /// <param name="setValue">
        /// An optional action that sets the value of the bound property. This is used if the binder supports reverse binding.
        /// </param>
        /// <returns>
        /// An interface for removing the binder from the ViewModel event.
        /// </returns>
        public static IRemoveBinderFromViewModel AddBinder<T>(IBinder binder, T value, ref ViewModelEvent<T>? viewModelEvent, Action<T>? setValue = null)
        {
            var isReverse = binder.IsReverseEnabled;
            viewModelEvent ??= new ViewModelEvent<T>();

            if (isReverse)
                viewModelEvent.SetValue ??= setValue;
            
            return viewModelEvent.AddBinder(binder, value, isReverse);
        }
    }
}