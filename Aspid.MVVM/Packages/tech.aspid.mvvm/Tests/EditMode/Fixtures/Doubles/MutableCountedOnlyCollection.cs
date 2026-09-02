using System;
using System.Collections;
using NUnit.Framework;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// The <see cref="ICollection{T}"/> counterpart of <see cref="CountedOnlyCollection"/> — the interface a
    /// collection written before <c>IReadOnlyCollection</c> existed offers, and the second counting
    /// fast path.
    /// </summary>
    internal sealed class MutableCountedOnlyCollection : ICollection<string>
    {
        public MutableCountedOnlyCollection(int count) => Count = count;

        public int Count { get; }

        public bool IsReadOnly => true;

        public void Add(string item) => throw new NotSupportedException();

        public void Clear() => throw new NotSupportedException();

        public bool Contains(string item) => throw new NotSupportedException();

        public void CopyTo(string[] array, int arrayIndex) => throw new NotSupportedException();

        public bool Remove(string item) => throw new NotSupportedException();

        public IEnumerator<string> GetEnumerator() => throw new AssertionException("the sequence was walked");

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
