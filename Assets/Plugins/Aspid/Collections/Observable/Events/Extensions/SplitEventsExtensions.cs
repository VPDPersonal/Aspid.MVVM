using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.Collections.Observable
{
    public static class SplitEventsExtensions
    {
        public static IObservableEvents<T> SplitByEvents<T>(
            this IObservableCollection<T> observableCollection,
            Action<IReadOnlyList<T?>, int>? added = null,
            Action<IReadOnlyList<T?>, int>? removed = null,
            Action<IReadOnlyList<T?>, int, int>? moved = null,
            Action<IReadOnlyList<T?>, IReadOnlyList<T?>, int>? replaced = null,
            Action? reset = null)
        {
            return new ObservableCollectionEvents<T>(observableCollection, added, removed, moved, replaced, reset);
        }
    }
}