using System;
using System.Collections.Generic;

namespace Aspid.Collections.Observable.Filtered
{
    public static class CreateFilteredExtensions
    {
        public static FilteredList<T> CreateFiltered<T>(this IReadOnlyList<T> list) => new(list);
        
        public static FilteredList<T> CreateFiltered<T>(this IReadOnlyList<T> list, IComparer<T>? comparer, Predicate<T>? filter = null)
            => new(list, comparer, filter);
        
        public static FilteredList<T> CreateFiltered<T>(this IReadOnlyList<T> list, Predicate<T>? filter, IComparer<T>? comparer = null)
            => new(list, filter, comparer);
    }
}