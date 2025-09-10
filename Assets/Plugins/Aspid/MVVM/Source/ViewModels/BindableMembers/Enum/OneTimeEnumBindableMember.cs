using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Represents a bindable member event that provides a single value in a one-time binding operation.
    /// </summary>
    /// <typeparam name="T">The type of the value to be bound.</typeparam>
    public sealed class OneTimeEnumBindableMember<T> : OneTimeStructBindableMember<T, Enum>
        where T : struct, Enum
    {
        private static readonly OneTimeEnumBindableMember<T> _instance = new();
    
        private OneTimeEnumBindableMember() { }
    
        public static OneTimeEnumBindableMember<T> Get(T value)
        {
            _instance.Value = value;
            return _instance;
        }
    }
}