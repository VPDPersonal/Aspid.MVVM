using System;

namespace Aspid.Collections.Observable.Synchronizer
{
    public static class CreateSyncExtensions
    {
        public static IReadOnlyObservableListSync<TTo> CreateSync<TFrom, TTo>(
            this IReadOnlyObservableList<TFrom> fromList, 
            Func<TFrom, TTo> converter,
            bool isDisposable = false)
        {
            return new ObservableListSync<TFrom, TTo>(fromList, converter, isDisposable);
        }
        
        public static IReadOnlyObservableListSync<TTo> CreateSync<TFrom, TTo>(
            this IReadOnlyObservableList<TFrom> fromList, 
            Func<TFrom, TTo> converter,
            Action<TTo> removed)
        {
            return new ObservableListSync<TFrom, TTo>(fromList, converter, removed);
        }
        
        public static IReadOnlyObservableCollectionSync<TTo> CreateSync<TFrom, TTo>(
            this ObservableQueue<TFrom> fromQueue, 
            Func<TFrom, TTo> converter,
            bool isDisposable = false)
        {
            return new ObservableQueueSync<TFrom, TTo>(fromQueue, converter, isDisposable);
        }
        
        public static IReadOnlyObservableCollectionSync<TTo> CreateSync<TFrom, TTo>(
            this ObservableQueue<TFrom> fromQueue, 
            Func<TFrom, TTo> converter,
            Action<TTo> remove)
        {
            return new ObservableQueueSync<TFrom, TTo>(fromQueue, converter, remove);
        }
        
        public static IReadOnlyObservableCollectionSync<TTo> CreateSync<TFrom, TTo>(
            this ObservableStack<TFrom> fromStack, 
            Func<TFrom, TTo> converter,
            bool isDisposable = false)
        {
            return new ObservableStackSync<TFrom, TTo>(fromStack, converter, isDisposable);
        }
        
        public static IReadOnlyObservableCollectionSync<TTo> CreateSync<TFrom, TTo>(
            this ObservableStack<TFrom> fromStack, 
            Func<TFrom, TTo> converter,
            Action<TTo> remove)
        {
            return new ObservableStackSync<TFrom, TTo>(fromStack, converter, remove);
        }

        public static IReadOnlyObservableCollectionSync<TTo> CreateSync<TFrom, TTo>(
            this ObservableHashSet<TFrom> fromHashSet,
            Func<TFrom, TTo> converter,
            bool isDisposable = false)
            where TTo : notnull
            where TFrom : notnull
        {
            return new ObservableHashSetSync<TFrom, TTo>(fromHashSet, converter, isDisposable);
        }

        public static IReadOnlyObservableCollectionSync<TTo> CreateSync<TFrom, TTo>(
            this ObservableHashSet<TFrom> fromHashSet,
            Func<TFrom, TTo> converter,
            Action<TTo> remove)
            where TTo : notnull
            where TFrom : notnull
        {
            return new ObservableHashSetSync<TFrom, TTo>(fromHashSet, converter, remove);
        }

        public static IReadOnlyObservableDictionarySync<TKey, TTo> CreateSync<TKey, TFrom, TTo>(
            this IReadOnlyObservableDictionary<TKey, TFrom> fromDictionary, 
            Func<TFrom, TTo> converter,
            bool isDisposable = false)
            where TKey : notnull
        {
            return new ObservableDictionarySync<TKey, TFrom, TTo>(fromDictionary, converter, isDisposable);
        }
        
        public static IReadOnlyObservableDictionarySync<TKey, TTo> CreateSync<TKey, TFrom, TTo>(
            this IReadOnlyObservableDictionary<TKey, TFrom> fromDictionary, 
            Func<TFrom, TTo> converter,
            Action<TTo> remove)
            where TKey : notnull
        {
            return new ObservableDictionarySync<TKey, TFrom, TTo>(fromDictionary, converter, remove);
        }
    }
}