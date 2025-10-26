using System;
using System.Collections.Generic;
using Aspid.Collections.Observable;
using System.Collections.Specialized;
using Aspid.Collections.Observable.Filtered;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    public abstract partial class ObservableListMonoBinder<T> : MonoBinder, 
        IBinder<IReadOnlyObservableList<T>>, IBinder<IReadOnlyFilteredList<T>>, IBinder<IReadOnlyList<T>>
    {
        protected IReadOnlyList<T> List { get; private set; }

        [BinderLog]
        public void SetValue(IReadOnlyList<T> list) =>
            InitializeList(list);
        
        [BinderLog]
        public void SetValue(IReadOnlyFilteredList<T> list) =>
            InitializeList(list);

        [BinderLog]
        public void SetValue(IReadOnlyObservableList<T> list) =>
            InitializeList(list);

        protected override void OnUnbound() =>
            DeinitializeList();

        private void InitializeList(IReadOnlyList<T> list)
        {
            DeinitializeList();

            List = list;
            if (List is null) return;
            List = GetFilterList(list) ?? list;
            
            OnAdded(List, 0);

            switch (List)
            {
                case IReadOnlyFilteredList<T> filteredList: filteredList.CollectionChanged += OnCollectionChanged; break;
                case IReadOnlyObservableList<T> observableList: observableList.CollectionChanged += OnCollectionChanged; break;
            }
        }

        private void DeinitializeList()
        {
            if (List is null) return;
            
            switch (List)
            {
                case IReadOnlyFilteredList<T> filteredList: filteredList.CollectionChanged -= OnCollectionChanged; break;
                case IReadOnlyObservableList<T> observableList: observableList.CollectionChanged -= OnCollectionChanged; break;
            }
            
            List = null;
            OnReset();
        }

        private void OnCollectionChanged()
        {
            OnReset();
            OnAdded(List, 0);
        }

        private void OnCollectionChanged(INotifyCollectionChangedEventArgs<T> e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    {
                        if (e.IsSingleItem) OnAdded(e.NewItem, e.NewStartingIndex);
                        else OnAdded(e.NewItems, e.NewStartingIndex);
                    }
                    break;
                
                case NotifyCollectionChangedAction.Remove:
                    {
                        if (e.IsSingleItem) OnRemoved(e.OldItem, e.OldStartingIndex);
                        else OnRemoved(e.OldItems, e.OldStartingIndex);
                    }
                    break;

                case NotifyCollectionChangedAction.Replace:
                    {
                        if (e.IsSingleItem) OnReplace(e.OldItem, e.NewItem, e.OldStartingIndex);
                        else throw new NotImplementedException();
                    }
                    break;

                case NotifyCollectionChangedAction.Reset:
                    {
                        OnReset();
                    }
                    break;

                case NotifyCollectionChangedAction.Move:
                    {
                        OnMove(e.OldItem, e.NewItem, e.OldStartingIndex, e.NewStartingIndex);
                    }
                    break;
                
                default: throw new ArgumentOutOfRangeException();
            }
        }
        
        protected virtual IReadOnlyFilteredList<T> GetFilterList(IReadOnlyList<T> list) => null;
        
        protected abstract void OnAdded(T newItem, int newStartingIndex);

        protected abstract void OnAdded(IReadOnlyList<T> newItems, int newStartingIndex);

        protected abstract void OnRemoved(T oldItem, int oldStartingIndex);

        protected abstract void OnRemoved(IReadOnlyList<T> oldItems, int oldStartingIndex);

        protected abstract void OnReplace(T oldItem, T newItem, int newStartingIndex);
        
        protected abstract void OnMove(T oldItem, T newItem, int oldStartingIndex, int newStartingIndex);

        protected abstract void OnReset();
    }
}