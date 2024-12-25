using System;
using System.Diagnostics;

namespace Aspid.MVVM.Mono.Views
{
    /// <summary>
    /// Attribute used to specify a required type for Binders
    /// that implement the <see cref="IMonoBinderValidable"/> interface.
    /// </summary>
    [Conditional("UNITY_EDITOR")]
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public sealed class RequireBinderAttribute : Attribute
    {
        /// <summary>
        /// The type that the Binder must support.
        /// </summary>
        /// <example>
        /// If the specified type is <c>string</c>, then the Binder must implement the interface <c>IBinder{string}</c>.
        /// This ensures that the Binder supports working with the specified type at runtime.
        /// </example>
        public Type Type { get; }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="RequireBinderAttribute"/> 
        /// with the specified type that must be supported by the Binder.
        /// </summary>
        /// <param name="type">The type that must be supported by the Binder.</param>
        public RequireBinderAttribute(Type type)
        {
            Type = type;
        }
    }
}