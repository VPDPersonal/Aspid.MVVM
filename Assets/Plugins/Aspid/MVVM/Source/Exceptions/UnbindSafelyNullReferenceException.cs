using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public sealed class UnbindSafelyNullReferenceException : NullReferenceException
    {
        public UnbindSafelyNullReferenceException() { }

        public UnbindSafelyNullReferenceException(string message) 
            : base(message) { }

        public UnbindSafelyNullReferenceException(string message, Exception innerException) 
            : base(message, innerException) { }
    }
}