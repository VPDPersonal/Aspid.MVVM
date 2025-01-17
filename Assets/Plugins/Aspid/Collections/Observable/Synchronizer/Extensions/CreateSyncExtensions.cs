using System;

namespace Aspid.Collections.Observable.Synchronizer
{
    public static class CreateSyncExtensions
    {
        public static ObservableListSync<TFrom, TTo> CreateSync<TFrom, TTo>(
            this IReadOnlyObservableList<TFrom> fromList, 
            out ObservableList<TTo> toList,
            Func<TFrom, TTo> converter)
        {
            return new ObservableListSync<TFrom, TTo>(fromList, out toList, converter);
        }
        
        public static ObservableQueueSync<TFrom, TTo> CreateSync<TFrom, TTo>(
            this ObservableQueue<TFrom> fromQueue, 
            out ObservableQueue<TTo> toQueue,
            Func<TFrom, TTo> converter)
        {
            return new ObservableQueueSync<TFrom, TTo>(fromQueue, out toQueue, converter);
        }
        
        public static ObservableStackSync<TFrom, TTo> CreateSync<TFrom, TTo>(
            this ObservableStack<TFrom> fromStack, 
            out ObservableStack<TTo> toStack,
            Func<TFrom, TTo> converter)
        {
            return new ObservableStackSync<TFrom, TTo>(fromStack, out toStack, converter);
        }
        
        public static ObservableDictionarySync<TKey, TFrom, TTo> CreateSync<TKey, TFrom, TTo>(
            this IReadOnlyObservableDictionary<TKey, TFrom> fromStack, 
            out ObservableDictionary<TKey, TTo> toStack,
            Func<TFrom, TTo> converter)
            where TKey : notnull
        {
            return new ObservableDictionarySync<TKey, TFrom, TTo>(fromStack, out toStack, converter);
        }
    }
}