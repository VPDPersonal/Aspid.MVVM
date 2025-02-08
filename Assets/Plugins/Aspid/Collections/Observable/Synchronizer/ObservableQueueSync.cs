using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Aspid.Collections.Observable.Synchronizer
{
    public sealed class ObservableQueueSync<TFrom, TTo> : IDisposable
    {
        private readonly Func<TFrom, TTo> _converter;
        private readonly ObservableQueue<TTo> _toQueue;
        private readonly ObservableQueue<TFrom> _fromQueue;

        public ObservableQueueSync(
            ObservableQueue<TFrom> fromQueue, 
            out ObservableQueue<TTo> toQueue,
            Func<TFrom, TTo> converter)
        {
            _converter = converter;
            _fromQueue = fromQueue;
            _toQueue = toQueue = new ObservableQueue<TTo>(fromQueue.Count);

            foreach (var from in fromQueue)
                toQueue.Enqueue(converter(from));

            Subscribe();
        }

        private void Subscribe() => 
            _fromQueue.CollectionChanged += OnFromQueueChanged;

        private void Unsubscribe() =>
            _fromQueue.CollectionChanged -= OnFromQueueChanged;
        
        private TTo[] Convert(IReadOnlyList<TFrom> fromValues)
        {
            var toValues = new TTo[fromValues.Count];

            for (var i = 0; i < toValues.Length; i++)
                toValues[i] = Convert(fromValues[i]);

            return toValues;
        }

        private TTo Convert(TFrom fromValue) => 
            _converter(fromValue);

        private void OnFromQueueChanged(INotifyCollectionChangedEventArgs<TFrom> args)
        {
            switch (args.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    {
                        if (args.IsSingleItem) _toQueue.Enqueue(Convert(args.NewItem!));
                        else _toQueue.EnqueueRange(Convert(args.NewItems!));
                    }
                    break;
                
                case NotifyCollectionChangedAction.Remove:
                    {
                        if (args.IsSingleItem) _toQueue.Dequeue();
                        _toQueue.DequeueRange(new TTo[args.OldItems!.Count]);
                    }
                    break;

                case NotifyCollectionChangedAction.Reset:
                    {
                        _toQueue.Clear();
                    }
                    break;

                case NotifyCollectionChangedAction.Move:
                case NotifyCollectionChangedAction.Replace:
                    throw new NotImplementedException();
                    
                default: throw new ArgumentOutOfRangeException();
            }
        }

        public void Dispose() => 
            Unsubscribe();
    }
}