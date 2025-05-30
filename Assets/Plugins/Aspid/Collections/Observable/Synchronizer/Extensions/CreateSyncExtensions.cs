using System;

namespace Aspid.Collections.Observable.Synchronizer
{
    public static class CreateSyncExtensions
    {
        public static IReadOnlyObservableList<TTo> CreateSync<TFrom, TTo>(
            this IReadOnlyObservableList<TFrom> fromList, 
            Func<TFrom, TTo> converter,
            bool isDisposable = false)
        {
            return new ObservableListSync<TFrom, TTo>(fromList, converter, isDisposable);
        }
        
        public static IReadOnlyObservableList<TTo> CreateSync<TFrom, TTo>(
            this IReadOnlyObservableList<TFrom> fromList, 
            Func<TFrom, TTo> converter,
            Action<TTo> removed)
        {
            return new ObservableListSync<TFrom, TTo>(fromList, converter, removed);
        }
        
        public static IReadOnlyObservableCollection<TTo> CreateSync<TFrom, TTo>(
            this ObservableQueue<TFrom> fromQueue, 
            Func<TFrom, TTo> converter,
            bool isDisposable = false)
        {
            return new ObservableQueueSync<TFrom, TTo>(fromQueue, converter, isDisposable);
        }
        
        public static IReadOnlyObservableCollection<TTo> CreateSync<TFrom, TTo>(
            this ObservableQueue<TFrom> fromQueue, 
            Func<TFrom, TTo> converter,
            Action<TTo> remove)
        {
            return new ObservableQueueSync<TFrom, TTo>(fromQueue, converter, remove);
        }
        
        public static IReadOnlyObservableCollection<TTo> CreateSync<TFrom, TTo>(
            this ObservableStack<TFrom> fromStack, 
            Func<TFrom, TTo> converter,
            bool isDisposable = false)
        {
            return new ObservableStackSync<TFrom, TTo>(fromStack, converter, isDisposable);
        }
        
        public static IReadOnlyObservableCollection<TTo> CreateSync<TFrom, TTo>(
            this ObservableStack<TFrom> fromStack, 
            Func<TFrom, TTo> converter,
            Action<TTo> remove)
        {
            return new ObservableStackSync<TFrom, TTo>(fromStack, converter, remove);
        }
        
        public static IReadOnlyObservableCollection<TTo> CreateSync<TFrom, TTo>(
            this ObservableHashSet<TFrom> fromStack, 
            Func<TFrom, TTo> converter,
            bool isDisposable = false) 
            where TTo : notnull
            where TFrom : notnull 
        {
            return new ObservableHashSetSync<TFrom, TTo>(fromStack, converter, isDisposable);
        }
        
        public static IReadOnlyObservableCollection<TTo> CreateSync<TFrom, TTo>(
            this ObservableHashSet<TFrom> fromStack, 
            Func<TFrom, TTo> converter,
            Action<TTo> remove) 
            where TTo : notnull
            where TFrom : notnull 
        {
            return new ObservableHashSetSync<TFrom, TTo>(fromStack, converter, remove);
        }
        
        public static IReadOnlyObservableDictionary<TKey, TTo> CreateSync<TKey, TFrom, TTo>(
            this IReadOnlyObservableDictionary<TKey, TFrom> fromStack, 
            Func<TFrom, TTo> converter,
            bool isDisposable = false)
            where TKey : notnull
        {
            return new ObservableDictionarySync<TKey, TFrom, TTo>(fromStack, converter, isDisposable);
        }
        
        public static IReadOnlyObservableDictionary<TKey, TTo> CreateSync<TKey, TFrom, TTo>(
            this IReadOnlyObservableDictionary<TKey, TFrom> fromStack, 
            Func<TFrom, TTo> converter,
            Action<TTo> remove)
            where TKey : notnull
        {
            return new ObservableDictionarySync<TKey, TFrom, TTo>(fromStack, converter, remove);
        }
    }
}