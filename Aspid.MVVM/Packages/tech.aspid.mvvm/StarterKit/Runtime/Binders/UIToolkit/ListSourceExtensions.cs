using System;
using System.Collections;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Wraps a read-only collection as the <see cref="IList"/> a <see cref="UnityEngine.UIElements.ListView"/> takes.
    /// </summary>
    public static class ListSourceExtensions
    {
        /// <summary>
        /// Wraps <paramref name="list"/> without copying; every mutating member throws.
        /// </summary>
        /// <param name="list">The collection to wrap, or <see langword="null"/>.</param>
        /// <returns>The <see cref="IList"/> view, or <see langword="null"/>.</returns>
        public static IList ToListSource(this IReadOnlyList<object> list) =>
            list is null ? null : new ReadOnlyListSource(list);

        private sealed class ReadOnlyListSource : IList
        {
            private readonly IReadOnlyList<object> _list;

            public ReadOnlyListSource(IReadOnlyList<object> list) =>
                _list = list;

            public int Count => _list.Count;
            public bool IsReadOnly => true;
            public bool IsFixedSize => false;
            public object SyncRoot => _list;
            public bool IsSynchronized => false;

            public object this[int index]
            {
                get => _list[index];
                set => throw ReadOnly();
            }

            public IEnumerator GetEnumerator() => _list.GetEnumerator();

            public bool Contains(object value) => IndexOf(value) >= 0;

            public int IndexOf(object value)
            {
                for (var i = 0; i < _list.Count; i++)
                {
                    if (Equals(_list[i], value)) return i;
                }

                return -1;
            }

            public void CopyTo(Array array, int index)
            {
                for (var i = 0; i < _list.Count; i++)
                    array.SetValue(_list[i], index + i);
            }

            public int Add(object value) => throw ReadOnly();
            public void Clear() => throw ReadOnly();
            public void Insert(int index, object value) => throw ReadOnly();
            public void Remove(object value) => throw ReadOnly();
            public void RemoveAt(int index) => throw ReadOnly();

            private static NotSupportedException ReadOnly() =>
                new("The list view's source is the ViewModel's collection and is read-only.");
        }
    }
}
