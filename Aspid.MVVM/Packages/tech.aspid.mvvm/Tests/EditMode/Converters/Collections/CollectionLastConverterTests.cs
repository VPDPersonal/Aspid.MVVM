using System;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="CollectionLastConverter{T}"/> — the indexed and walked paths, the
    /// empty and null fallbacks, and the null-tail distinction.
    /// </summary>
    /// <remarks>
    /// Every behavioural assertion is made against both the indexed and the walking path; the walking
    /// path is pinned on how many items it pulls.
    /// </remarks>
    [TestFixture]
    public sealed class CollectionLastConverterTests
    {
        private static readonly string[] _three = { "a", "b", "c" };

        [Test]
        public void Last_List_ReturnsTheTail() =>
            Assert.AreEqual("c", new CollectionLastConverter<string>("?").Convert(_three));

        [Test]
        public void Last_WalkedSequence_ReturnsTheTail() =>
            Assert.AreEqual("c", new CollectionLastConverter<string>("?").Convert(Streamed("a", "b", "c")));

        [Test]
        public void Last_Queue_ReturnsTheTail() =>
            Assert.AreEqual("c", new CollectionLastConverter<string>("?").Convert(new Queue<string>(_three)));

        [Test]
        public void Last_EmptyList_ReturnsTheFallback() =>
            Assert.AreEqual("?", new CollectionLastConverter<string>("?").Convert(Array.Empty<string>()));

        // The walked branch never assigns when there is nothing to walk, so the fallback it started
        // from is what comes back. A branch seeded with default(T) would answer null here instead.
        [Test]
        public void Last_EmptyWalkedSequence_ReturnsTheFallback() =>
            Assert.AreEqual("?", new CollectionLastConverter<string>("?").Convert(Streamed<string>()));

        [Test]
        public void Last_Null_ReturnsTheFallback() =>
            Assert.AreEqual("?", new CollectionLastConverter<string>("?").Convert(null));

        [Test]
        public void Last_SingleItem_ReturnsIt()
        {
            Assert.AreEqual("a", new CollectionLastConverter<string>("?").Convert(new[] { "a" }));
            Assert.AreEqual("a", new CollectionLastConverter<string>("?").Convert(Streamed("a")));
        }

        // The mirror of the head case: a null tail overwrites the fallback the walk started from, so
        // "no last item" and "a last item that is null" are not the same answer.
        [Test]
        public void Last_NullTail_ReturnsNullRatherThanTheFallback()
        {
            Assert.IsNull(new CollectionLastConverter<string>("?").Convert(new[] { "a", null }));
            Assert.IsNull(new CollectionLastConverter<string>("?").Convert(Streamed("a", null)));
        }

        // The cost the documentation admits to: nothing short of walking the whole sequence can name
        // its last item. Pinned so that a later "optimisation" that stops early is caught.
        [Test]
        public void Last_WalkedSequence_PullsEveryItem()
        {
            var probe = new SequencePullProbe();

            Assert.AreEqual(100, new CollectionLastConverter<int>(-1).Convert(Counted(100, probe)));
            Assert.AreEqual(100, probe.Pulls);
        }

        private static IEnumerable<T> Streamed<T>(params T[] items)
        {
            foreach (var item in items)
                yield return item;
        }

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
