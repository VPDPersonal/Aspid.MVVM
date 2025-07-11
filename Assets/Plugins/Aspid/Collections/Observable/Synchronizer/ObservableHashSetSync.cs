using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Aspid.Collections.Observable.Synchronizer
{
    internal sealed class ObservableHashSetSync<TFrom, TTo> : ObservableHashSet<TTo>, IReadOnlyObservableCollectionSync<TTo>
        where TTo : notnull 
        where TFrom : notnull
    {
        private readonly bool _isDisposable;
        private readonly Action<TTo>? _remove;
        private readonly Func<TFrom, TTo> _converter;
        private readonly Dictionary<TFrom, TTo> _sync;
        private readonly ObservableHashSet<TFrom> _fromHasSet;

        public ObservableHashSetSync(
            ObservableHashSet<TFrom> fromHashSet, 
            Func<TFrom, TTo> converter,
            Action<TTo>? remove)
        {
            _remove = remove;
            _converter = converter;
            _fromHasSet = fromHashSet;
            _sync = new Dictionary<TFrom, TTo>(fromHashSet.Count);

            foreach (var from in fromHashSet)
            {
                var to = Convert(from);
                
                Add(to);
                _sync.Add(from,to );
            }

            Subscribe();
        }
        
        public ObservableHashSetSync(
            ObservableHashSet<TFrom> fromHashSet, 
            Func<TFrom, TTo> converter,
            bool isDisposable = false)
            : this(fromHashSet, converter, null)
        {
            _isDisposable = isDisposable;
        }

        private void Subscribe() => 
            _fromHasSet.CollectionChanged += OnFromStackChanged;

        private void Unsubscribe() =>
            _fromHasSet.CollectionChanged -= OnFromStackChanged;

        private TTo Convert(TFrom fromValue) => 
            _converter.Invoke(fromValue);

        private void OnFromStackChanged(INotifyCollectionChangedEventArgs<TFrom> args)
        {
            switch (args.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    {
                        if (args.IsSingleItem)
                        {
                            var fromItem = args.NewItem!;
                            var item = Convert(fromItem);
                            
                            Add(item);
                            _sync.Add(fromItem, item);
                        }
                        else throw new NotImplementedException();
                    }
                    break;
                
                case NotifyCollectionChangedAction.Remove:
                    {
                        if (args.IsSingleItem)
                        {
                            Remove(_sync[args.OldItem!]);
                            _sync.Remove(args.OldItem!);
                        }
                        else throw new NotImplementedException();
                    }
                    break;

                case NotifyCollectionChangedAction.Reset:
                    {
                        Clear();
                        _sync.Clear();
                    }
                    break;

                case NotifyCollectionChangedAction.Move:
                case NotifyCollectionChangedAction.Replace:
                    throw new NotImplementedException();
                    
                default: throw new ArgumentOutOfRangeException();
            }
        }
        
        protected override void OnRemoved(TTo item)
        {
            if (_isDisposable)
            {
                if (item is IDisposable disposable)
                    disposable.Dispose();
            }
            else _remove?.Invoke(item);
        }

        protected override void OnClearing()
        {
            if (_isDisposable)
            {
                foreach (var item in _sync.Values)
                {
                    if (item is IDisposable disposable)
                        disposable.Dispose();
                }
            }
            else if (_remove is not null)
            {
                foreach (var item in _sync.Values)
                    _remove.Invoke(item);
            }
        }

        public override void Dispose()
        {
            Unsubscribe();
            base.Dispose();
        }
    }
}