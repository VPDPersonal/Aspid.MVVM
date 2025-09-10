using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.Collections.Observable
{
    public abstract class CollectionChangedEvent<T>
    {
        private List<NotifyCollectionChangedEventHandler<T>>? _handlers;
        
        public event NotifyCollectionChangedEventHandler<T>? CollectionChanged
        {
            add
            {
                _handlers ??= new List<NotifyCollectionChangedEventHandler<T>>();
                _handlers.Add(value ?? throw ThrowValueNullReferenceException());
            }
            remove => _handlers?.Remove(value ?? throw ThrowValueNullReferenceException());
        }

        protected void Invoke(INotifyCollectionChangedEventArgs<T> e)
        {
            if (_handlers is null) return;
            
            foreach (var handler in _handlers)
                handler.Invoke(e);
        }

        private static NullReferenceException ThrowValueNullReferenceException() =>
            throw new NullReferenceException("value");
    }
}