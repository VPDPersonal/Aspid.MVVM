using System.Collections;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Provides the adapter a <see cref="UnityEngine.UIElements.ListView"/> needs from a read-only collection.
    /// </summary>
    /// <remarks>
    /// Wraps rather than copies, so a large collection costs one object. Every mutating member throws, since the
    /// ViewModel's collection is read-only.
    /// </remarks>
    public static class ListSourceExtensions
    {
        /// <summary>
        /// Wraps <paramref name="list"/> as the <see cref="IList"/> a list view takes as its source.
        /// </summary>
        /// <param name="list">The collection to wrap, or <see langword="null"/>.</param>
        /// <returns>An <see cref="IList"/> view over <paramref name="list"/>, or <see langword="null"/>.</returns>
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
                set => throw new System.NotSupportedException("The list view's source is the ViewModel's collection and is read-only.");
            }

            public IEnumerator GetEnumerator() => _list.GetEnumerator();

            public bool Contains(object value)
            {
                for (var i = 0; i < _list.Count; i++)
                {
                    if (Equals(_list[i], value)) return true;
                }

                return false;
            }

            public int IndexOf(object value)
            {
                for (var i = 0; i < _list.Count; i++)
                {
                    if (Equals(_list[i], value)) return i;
                }

                return -1;
            }

            public void CopyTo(System.Array array, int index)
            {
                for (var i = 0; i < _list.Count; i++)
                    array.SetValue(_list[i], index + i);
            }

            public int Add(object value) => throw new System.NotSupportedException("The list view's source is the ViewModel's collection and is read-only.");

            public void Clear() => throw new System.NotSupportedException("The list view's source is the ViewModel's collection and is read-only.");

            public void Insert(int index, object value) => throw new System.NotSupportedException("The list view's source is the ViewModel's collection and is read-only.");

            public void Remove(object value) => throw new System.NotSupportedException("The list view's source is the ViewModel's collection and is read-only.");

            public void RemoveAt(int index) => throw new System.NotSupportedException("The list view's source is the ViewModel's collection and is read-only.");
        }
    }
}
