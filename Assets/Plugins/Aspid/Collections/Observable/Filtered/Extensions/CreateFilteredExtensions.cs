using System;
using System.Collections.Generic;

namespace Aspid.Collections.Observable.Filtered
{
    public static class CreateFilteredExtensions
    {
        public static FilteredList<T> CreateFiltered<T>(IReadOnlyList<T> list) => new(list);
        
        public static FilteredList<T> CreateFiltered<T>(IReadOnlyList<T> list, IComparer<T>? comparer, Func<T, bool>? filter = null)
            => new(list, comparer, filter);
        
        public static FilteredList<T> CreateFiltered<T>(IReadOnlyList<T> list, Func<T, bool>? filter, IComparer<T>? comparer = null)
            => new(list, filter, comparer);
    }
}