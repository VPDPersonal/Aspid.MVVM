using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Exception thrown when a <see langword="null"/> binder is encountered during a
    /// <see cref="BinderExtensions.BindSafely{T}(T, IBinderAdder, object, string)"/> operation.
    /// </summary>
    public sealed class BindSafelyNullReferenceException : NullReferenceException
    {
        /// <inheritdoc/>
        public BindSafelyNullReferenceException() { }

        /// <inheritdoc/>
        public BindSafelyNullReferenceException(string message)
            : base(message) { }

        /// <inheritdoc/>
        public BindSafelyNullReferenceException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
