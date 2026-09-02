using System;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="CollectionFirstConverter{T}"/> — the indexed and walked paths, the
    /// empty and null fallbacks, and the null-head distinction.
    /// </summary>
    /// <remarks>
    /// Every behavioural assertion is made against both the indexed and the walking path.
    /// </remarks>
    [TestFixture]
    public sealed class CollectionFirstConverterTests
    {
        private static readonly string[] _three = { "a", "b", "c" };

        [Test]
        public void First_List_ReturnsTheHead() =>
            Assert.AreEqual("a", new CollectionFirstConverter<string>("?").Convert(_three));

        // The same three items behind an enumerator with no indexer take the foreach branch instead.
        [Test]
        public void First_WalkedSequence_ReturnsTheHead() =>
            Assert.AreEqual("a", new CollectionFirstConverter<string>("?").Convert(Streamed("a", "b", "c")));

        // A queue is the shape the class documentation names: a real collection that stops at
        // IReadOnlyCollection and so never reaches the indexed branch.
        [Test]
        public void First_Queue_ReturnsTheHead() =>
            Assert.AreEqual("a", new CollectionFirstConverter<string>("?").Convert(new Queue<string>(_three)));

        [Test]
        public void First_EmptyList_ReturnsTheFallback() =>
            Assert.AreEqual("?", new CollectionFirstConverter<string>("?").Convert(Array.Empty<string>()));

        [Test]
        public void First_EmptyWalkedSequence_ReturnsTheFallback() =>
            Assert.AreEqual("?", new CollectionFirstConverter<string>("?").Convert(Streamed<string>()));

        [Test]
        public void First_Null_ReturnsTheFallback() =>
            Assert.AreEqual("?", new CollectionFirstConverter<string>("?").Convert(null));

        [Test]
        public void First_DefaultConstructed_EmptySequence_ReturnsTheTypeDefault() =>
            Assert.IsNull(new CollectionFirstConverter<string>().Convert(Array.Empty<string>()));

        // The fallback answers an empty sequence, not an empty item. A sequence whose head is null is
        // not empty, so the null travels to the binder — the caller that authored a fallback to avoid
        // exactly that still gets it.
        [Test]
        public void First_NullHead_ReturnsNullRatherThanTheFallback()
        {
            Assert.IsNull(new CollectionFirstConverter<string>("?").Convert(new[] { null, "b" }));
            Assert.IsNull(new CollectionFirstConverter<string>("?").Convert(Streamed(null, "b")));
        }

        // The point of the converter: the head is answered without the length of the sequence
        // mattering. One pull, on a hundred available items.
        [Test]
        public void First_WalkedSequence_PullsOnlyTheHead()
        {
            var probe = new SequencePullProbe();

            Assert.AreEqual(1, new CollectionFirstConverter<int>(-1).Convert(Counted(100, probe)));
            Assert.AreEqual(1, probe.Pulls);
        }

        // Returning out of a foreach still runs the enumerator's Dispose, which a bare MoveNext would
        // skip — and an iterator holding a file handle or a pooled buffer frees it there.
        [Test]
        public void First_WalkedSequence_DisposesTheEnumeratorOnTheEarlyReturn()
        {
            var probe = new SequencePullProbe();

            new CollectionFirstConverter<int>(-1).Convert(Counted(100, probe));

            Assert.IsTrue(probe.Disposed);
        }

        // An iterator, so the result is an IEnumerable and nothing more — the converter's indexed fast
        // path cannot see it and the walking branch is the one under test.
        private static IEnumerable<T> Streamed<T>(params T[] items)
        {
            foreach (var item in items)
                yield return item;
        }

        // The same, with the pulls counted and the disposal recorded.
        private static IEnumerable<int> Counted(int length, SequencePullProbe probe)
        {
            try
            {
                for (var i = 1; i <= length; i++)
                {
                    probe.Pulls++;
                    yield return i;
                }
            }
            finally
            {
                probe.Disposed = true;
            }
        }
    }
}
