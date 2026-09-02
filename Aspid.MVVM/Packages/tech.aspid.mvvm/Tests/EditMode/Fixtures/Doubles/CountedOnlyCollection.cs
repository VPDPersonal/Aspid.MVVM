using System.Collections;
using NUnit.Framework;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// A collection that answers <see cref="Count"/> and throws on being walked, so a converter's
    /// counting fast path is asserted rather than assumed.
    /// </summary>
    internal sealed class CountedOnlyCollection : IReadOnlyCollection<string>
    {
        public CountedOnlyCollection(int count) => Count = count;

        public int Count { get; }

        public IEnumerator<string> GetEnumerator() => throw new AssertionException("the sequence was walked");

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
