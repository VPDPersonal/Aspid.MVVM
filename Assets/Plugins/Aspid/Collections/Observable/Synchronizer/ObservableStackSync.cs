using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Aspid.Collections.Observable.Synchronizer
{
    public sealed class ObservableStackSync<TFrom, TTo> : IDisposable
    {
        private readonly Func<TFrom, TTo> _converter;
        private readonly ObservableStack<TTo> _toStack;
        private readonly ObservableStack<TFrom> _fromStack;

        public ObservableStackSync(
            ObservableStack<TFrom> fromStack, 
            out ObservableStack<TTo> toStack,
            Func<TFrom, TTo> converter)
        {
            _converter = converter;
            _fromStack = fromStack;
            _toStack = toStack = new ObservableStack<TTo>(fromStack.Count);

            foreach (var from in fromStack)
                toStack.Push(_converter(from));

            Subscribe();
        }

        private void Subscribe() => 
            _fromStack.CollectionChanged += OnFromStackChanged;

        private void Unsubscribe() =>
            _fromStack.CollectionChanged -= OnFromStackChanged;
        
        private TTo[] Convert(IReadOnlyList<TFrom> fromValues)
        {
            var toValues = new TTo[fromValues.Count];

            for (var i = 0; i < toValues.Length; i++)
                toValues[i] = Convert(fromValues[i]);

            return toValues;
        }

        private TTo Convert(TFrom fromValue) => 
            _converter(fromValue);

        private void OnFromStackChanged(INotifyCollectionChangedEventArgs<TFrom> args)
        {
            switch (args.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    {
                        if (args.IsSingleItem) _toStack.Push(Convert(args.NewItem!));
                        else _toStack.PushRange(Convert(args.NewItems!));
                    }
                    break;
                
                case NotifyCollectionChangedAction.Remove:
                    {
                        if (args.IsSingleItem) _toStack.Pop();
                        _toStack.PopRange(new TTo[args.OldItems!.Count]);
                    }
                    break;

                case NotifyCollectionChangedAction.Reset:
                    {
                        _toStack.Clear();
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