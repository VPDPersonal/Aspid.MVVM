#nullable enable
using System;
using System.Collections.Generic;
using Aspid.Collections.Observable;
using System.Collections.Specialized;

namespace Aspid.MVVM.StarterKit.Binders
{
    public abstract class DictionaryBinderBase<TKey, TValue> : Binder,
        IBinder<IReadOnlyObservableDictionary<TKey, TValue?>>, IDisposable
    {
        private IReadOnlyObservableDictionary<TKey, TValue?>? _dictionary;
        
        public void SetValue(IReadOnlyObservableDictionary<TKey, TValue?>? dictionary)
        {
            if (_dictionary is not null)
            {
                OnReset();
                Unsubscribe();
            }
            
            _dictionary = dictionary;
            
            if (dictionary is null) return;
            if (dictionary.Count > 0)
            {
                foreach (var pair in dictionary)
                    OnAdded(pair);
            }
            
            Subscribe();
        }

        private void Subscribe() => 
            _dictionary!.CollectionChanged += OnCollectionChanged;

        private void Unsubscribe() => 
            _dictionary!.CollectionChanged -= OnCollectionChanged;
        
        private void OnCollectionChanged(INotifyCollectionChangedEventArgs<KeyValuePair<TKey, TValue?>> e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    {
                        if (e.IsSingleItem) OnAdded(e.NewItem);
                        else OnAdded(e.NewItems!);
                    }
                    break;
                
                case NotifyCollectionChangedAction.Remove:
                    {
                        if (e.IsSingleItem) OnRemoved(e.OldItem);
                        else OnRemoved(e.OldItems!);
                    }
                    break;
                
                case NotifyCollectionChangedAction.Replace:
                    {
                        if (e.IsSingleItem) OnReplace(e.OldItem, e.NewItem);
                        else throw new NotImplementedException();
                    }
                    break;
                
                case NotifyCollectionChangedAction.Reset:
                    {
                        OnReset();
                    }
                    break;
                
                case NotifyCollectionChangedAction.Move: throw new NotImplementedException();
                default: throw new ArgumentOutOfRangeException();
            }
        }

        protected abstract void OnAdded(KeyValuePair<TKey, TValue?> newItem);

        protected abstract void OnAdded(IReadOnlyList<KeyValuePair<TKey, TValue?>> newItems);

        protected abstract void OnRemoved(KeyValuePair<TKey, TValue?> oldItem);

        protected abstract void OnRemoved(IReadOnlyList<KeyValuePair<TKey, TValue?>> oldItems);

        protected abstract void OnReplace(KeyValuePair<TKey, TValue?> oldItem, KeyValuePair<TKey, TValue?> newItem);

        protected abstract void OnReset();

        public virtual void Dispose()
        {
            if (_dictionary == null) return;
            OnReset();
            Unsubscribe();
        }
    }
}