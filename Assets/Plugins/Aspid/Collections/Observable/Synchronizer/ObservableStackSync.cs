using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Aspid.Collections.Observable.Synchronizer
{
    internal sealed class ObservableStackSync<TFrom, TTo> : ObservableStack<TTo>, IReadOnlyObservableCollectionSync<TTo>
    {
        private readonly bool _isDisposable;
        private readonly Action<TTo>? _remove;
        private readonly Func<TFrom, TTo> _converter;
        private readonly ObservableStack<TFrom> _fromStack;

        public ObservableStackSync(
            ObservableStack<TFrom> fromStack, 
            Func<TFrom, TTo> converter,
            Action<TTo>? remove)
        {
            _remove = remove;
            _converter = converter;
            _fromStack = fromStack;

            foreach (var from in fromStack)
                Push(_converter(from));

            Subscribe();
        }
        
        public ObservableStackSync(
            ObservableStack<TFrom> fromStack, 
            Func<TFrom, TTo> converter,
            bool isDisposable = false)
            : this(fromStack, converter, null)
        {
            _isDisposable = isDisposable;
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
                        if (args.IsSingleItem) Push(Convert(args.NewItem!));
                        else PushRange(Convert(args.NewItems!));
                    }
                    break;
                
                case NotifyCollectionChangedAction.Remove:
                    {
                        if (args.IsSingleItem) Pop();
                        else PopRange(new TTo[args.OldItems!.Count]);
                    }
                    break;

                case NotifyCollectionChangedAction.Reset:
                    {
                        Clear();
                    }
                    break;

                case NotifyCollectionChangedAction.Move:
                case NotifyCollectionChangedAction.Replace:
                    throw new NotImplementedException();
                    
                default: throw new ArgumentOutOfRangeException();
            }
        }

        protected override void OnPopped(TTo item)
        {
            if (_isDisposable)
            {
                if (item is IDisposable disposable) 
                    disposable.Dispose();
            }
            else _remove?.Invoke(item);
        }

        protected override void OnPoppedRange(in IReadOnlyList<TTo> dest) =>
            OnPoppedRange(dest);
        
        private void OnPoppedRange(IReadOnlyCollection<TTo> dest)
        {
            if (_isDisposable)
            {
                foreach (var item in dest)
                {
                    if (item is IDisposable disposable) 
                        disposable.Dispose();
                }
            }
            else if (_remove is not null)
            {
                foreach (var item in dest)
                    _remove(item);
            }
        }

        protected override void OnClearing() =>
            OnPoppedRange(this);

        public override void Dispose()
        {
            Unsubscribe();
            base.Dispose();
        }
    }
}