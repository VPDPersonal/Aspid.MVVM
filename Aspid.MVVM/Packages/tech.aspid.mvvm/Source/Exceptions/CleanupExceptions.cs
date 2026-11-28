using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal struct CleanupExceptions
    {
        private Exception? _first;
        private List<Exception>? _multiple;

        public void Add(Exception exception)
        {
            if (_first is null)
            {
                _first = exception;
            }
            else if (_multiple is not null)
            {
                _multiple.Add(exception);
            }
            else
            {
                _multiple = new List<Exception>(capacity: 2) { _first, exception };
            }
        }

        public readonly void ThrowIfAny()
        {
            if (_multiple is not null)
                throw new AggregateException(_multiple);

            if (_first is not null)
                ExceptionDispatchInfo.Capture(_first).Throw();
        }
    }
}
