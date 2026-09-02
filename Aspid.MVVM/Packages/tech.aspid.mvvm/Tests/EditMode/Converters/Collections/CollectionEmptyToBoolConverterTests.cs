using System;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="CollectionEmptyToBoolConverter{T}"/> — the counted and walked-sequence
    /// members, and its counting fast paths.
    /// </summary>
    [TestFixture]
    public sealed class CollectionEmptyToBoolConverterTests
    {
        [Test]
        public void EmptyToBool_ReportsEmptiness()
        {
            Assert.IsTrue(new CollectionEmptyToBoolConverter<string>().Convert(Array.Empty<string>()));
            Assert.IsTrue(new CollectionEmptyToBoolConverter<string>().Convert(null));
            Assert.IsFalse(new CollectionEmptyToBoolConverter<string>().Convert(new[] { "a", "b", "c" }));
        }

        [Test]
        public void EmptyToBool_WalkedSequence_ReportsEmptiness()
        {
            Assert.IsTrue(SequenceEmptiness().Convert(Walk<string>()));
            Assert.IsTrue(SequenceEmptiness().Convert(null));
            Assert.IsFalse(SequenceEmptiness().Convert(Walk("a", "b", "c")));
        }

        [Test]
        public void EmptyToBool_WalkedSequence_InvertsWhenAsked()
        {
            Assert.IsFalse(SequenceEmptiness(isInvert: true).Convert(Walk<string>()));
            Assert.IsTrue(SequenceEmptiness(isInvert: true).Convert(Walk("a")));
        }

        // Emptiness is one item's worth of question however long the sequence is, and the enumerator
        // is disposed on the way out — which an iterator holding a handle or a pooled buffer needs.
        [Test]
        public void EmptyToBool_WalkedSequence_PullsOneItemAndDisposes()
        {
            var probe = new SequencePullProbe();

            Assert.IsFalse(SequenceEmptiness().Convert(Counted(100, probe)));
            Assert.AreEqual(1, probe.Pulls);
            Assert.IsTrue(probe.Disposed);
        }

        [Test]
        public void EmptyToBool_List_ArrivingAsASequence_IsStillTested() =>
            Assert.IsFalse(SequenceEmptiness().Convert(new List<string> { "a" }));

        [Test]
        public void EmptyToBool_SequenceWithACount_TakesItRatherThanWalking()
        {
            Assert.IsFalse(SequenceEmptiness().Convert(new CountedOnlyCollection(3)));
            Assert.IsTrue(SequenceEmptiness().Convert(new CountedOnlyCollection(0)));
            Assert.IsFalse(SequenceEmptiness().Convert(new MutableCountedOnlyCollection(3)));
            Assert.IsTrue(SequenceEmptiness().Convert(new MutableCountedOnlyCollection(0)));
        }

        private static IConverter<IEnumerable<string>, bool> SequenceEmptiness(bool isInvert = false) =>
            new CollectionEmptyToBoolConverter<string>(isInvert);

        private static IEnumerable<T> Walk<T>(params T[] items)
        {
            foreach (var item in items)
                yield return item;
        }

        // The same, with the pulls counted and the disposal recorded. The items themselves are of no
        // interest — how many of them are asked for is the assertion.
        private static IEnumerable<string> Counted(int length, SequencePullProbe probe)
        {
            try
            {
                for (var i = 0; i < length; i++)
                {
                    probe.Pulls++;
                    yield return "item";
                }
            }
            finally
            {
                probe.Disposed = true;
            }
        }
    }
}
