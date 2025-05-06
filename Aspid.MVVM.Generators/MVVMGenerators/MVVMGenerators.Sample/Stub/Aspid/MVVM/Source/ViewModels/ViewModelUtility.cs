using System;
using System.Collections.Generic;

namespace Aspid.MVVM
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
            if (comparer is null) throw new ArgumentNullException(nameof(comparer));
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
            if (callback is null) throw new ArgumentNullException(nameof(callback));
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
            if (comparer is null) throw new ArgumentNullException(nameof(comparer));
            if (callback is null) throw new ArgumentNullException(nameof(callback));
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
    }
}