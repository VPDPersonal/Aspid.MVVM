using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public sealed class BindSafelyNullReferenceException : NullReferenceException
    {
        public BindSafelyNullReferenceException() { }

        public BindSafelyNullReferenceException(string message) 
            : base(message) { }

        public BindSafelyNullReferenceException(string message, Exception innerException) 
            : base(message, innerException) { }
    }
}