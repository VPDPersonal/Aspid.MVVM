using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Exception thrown when a <see langword="null"/> binder is encountered during an <c>UnbindSafely</c> operation.
    /// </summary>
    public sealed class UnbindSafelyNullReferenceException : NullReferenceException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnbindSafelyNullReferenceException"/> class.
        /// </summary>
        public UnbindSafelyNullReferenceException() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnbindSafelyNullReferenceException"/> class with the specified message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public UnbindSafelyNullReferenceException(string message)
            : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnbindSafelyNullReferenceException"/> class with the specified message and inner exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public UnbindSafelyNullReferenceException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
