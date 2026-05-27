using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Exception thrown when a <see langword="null"/> binder is encountered during an
    /// <see cref="BinderExtensions.UnbindSafely{T}(T, object, string)"/> operation.
    /// </summary>
    public sealed class UnbindSafelyNullReferenceException : NullReferenceException
    {
        /// <inheritdoc/>
        public UnbindSafelyNullReferenceException() { }

        /// <inheritdoc/>
        public UnbindSafelyNullReferenceException(string message)
            : base(message) { }

        /// <inheritdoc/>
        public UnbindSafelyNullReferenceException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
