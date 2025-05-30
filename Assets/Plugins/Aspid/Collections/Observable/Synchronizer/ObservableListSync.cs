using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Aspid.Collections.Observable.Synchronizer
{
    public sealed class ObservableListSync<TFrom, TTo> : ObservableList<TTo>
    {
        private readonly bool _isDisposable;
        private readonly Action<TTo>? _remove;
        private readonly Func<TFrom, TTo> _converter;
        private readonly IReadOnlyObservableList<TFrom> _fromList;

        public ObservableListSync(
            IReadOnlyObservableList<TFrom> fromList, 
            Func<TFrom, TTo> converter,
            Action<TTo>? remove)
        {
            _remove = remove;
            _fromList = fromList;
            _converter = converter;
            
            foreach (var from in fromList)
                Add(converter(from));
            
            Subscribe();
        }
        
        public ObservableListSync(
            IReadOnlyObservableList<TFrom> fromList, 
            Func<TFrom, TTo> converter,
            bool isDisposable = false)
            : this(fromList, converter, null)
        {
            _isDisposable = isDisposable;
        }
        
        private void Subscribe() => 
            _fromList.CollectionChanged += OnFromListChanged;
        
        private void Unsubscribe() => 
            _fromList.CollectionChanged -= OnFromListChanged;
        
        private TTo[] Convert(IReadOnlyList<TFrom> fromValues)
        {
            var toValues = new TTo[fromValues.Count];
        
            for (var i = 0; i < toValues.Length; i++)
                toValues[i] = Convert(fromValues[i]);
        
            return toValues;
        }
        
        private TTo Convert(TFrom fromValue) => 
            _converter(fromValue);
        
        private void OnFromListChanged(INotifyCollectionChangedEventArgs<TFrom> args)
        {
            switch (args.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    {
                        if (args.IsSingleItem) Add(Convert(args.NewItem!));
                        else AddRange(Convert(args.NewItems!));
                    }
                    break;
                
                case NotifyCollectionChangedAction.Move:
                    {
                        if (args.IsSingleItem) Move(args.OldStartingIndex, args.NewStartingIndex);
                        else throw new NotImplementedException();
                    }
                    break;
                
                case NotifyCollectionChangedAction.Remove:
                    {
                        if (args.IsSingleItem) RemoveAt(args.OldStartingIndex);
                        else throw new NotImplementedException();
                    }
                    break;
        
                case NotifyCollectionChangedAction.Replace:
                    {
                        if (args.IsSingleItem) base[args.OldStartingIndex] = Convert(args.NewItem!);
                        else throw new NotImplementedException();
                    }
                    break;
        
                case NotifyCollectionChangedAction.Reset:
                    {
                        Clear();
                    }
                    break;
                
                default: throw new ArgumentOutOfRangeException();
            }
        }

        protected override void OnReplaced(int index, in TTo oldItem, in TTo newItem) =>
            OnRemoved(oldItem);

        protected override void OnRemoved(in TTo value)
        {
            if (_isDisposable)
            {
                if (value is IDisposable disposable)
                    disposable.Dispose();
            }
            else _remove?.Invoke(value);
        }

        protected override void OnClearing()
        {
            if (_isDisposable)
            {
                foreach (var value in this)
                {
                    if (value is IDisposable disposable)
                        disposable.Dispose();
                }
            }
            else if (_remove is not null)
            {
                foreach (var value in this)
                    _remove.Invoke(value);
            }
        }
        
        public override void Dispose()
        {
            Unsubscribe();
            base.Dispose();
        }
    }
}