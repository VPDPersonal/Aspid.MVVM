using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Exception thrown when a <c>null</c> binder is encountered during a <c>BindSafely</c> operation.
    /// </summary>
    public sealed class BindSafelyNullReferenceException : NullReferenceException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BindSafelyNullReferenceException"/> class.
        /// </summary>
        public BindSafelyNullReferenceException() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BindSafelyNullReferenceException"/> class with the specified message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public BindSafelyNullReferenceException(string message)
            : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BindSafelyNullReferenceException"/> class with the specified message and inner exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public BindSafelyNullReferenceException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}