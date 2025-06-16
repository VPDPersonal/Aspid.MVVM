using System;
using System.Collections.Generic;
using Aspid.Collections.Observable;
using System.Collections.Specialized;

namespace Aspid.MVVM.StarterKit
{
    public abstract class DictionaryBinderBase<TKey, TValue> : Binder, IBinder<IReadOnlyObservableDictionary<TKey, TValue?>>
    {
        protected IReadOnlyObservableDictionary<TKey, TValue?>? Dictionary { get; private set; }

        protected DictionaryBinderBase(BindMode mode) 
            : base(mode) { }

        public void SetValue(IReadOnlyObservableDictionary<TKey, TValue?>? dictionary)
        {
            DeinitializeDictionary();
            
            Dictionary = dictionary;
            
            if (dictionary is null) return;
            if (dictionary.Count > 0)
            {
                foreach (var pair in dictionary)
                    OnAdded(pair);
            }
            
            InitializeDictionary();
        }
        
        protected override void OnUnbound() =>
            DeinitializeDictionary();

        private void InitializeDictionary() => 
            Dictionary!.CollectionChanged += OnCollectionChanged;

        private void DeinitializeDictionary()
        {
            if (Dictionary is null) return;
            
            OnReset();
            Dictionary!.CollectionChanged -= OnCollectionChanged;
        }

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
    }
}