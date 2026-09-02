using NUnit.Framework;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="CollectionCountConverter{T}"/> — the counted-collection member, the
    /// walked-sequence member, and its counting fast paths.
    /// </summary>
    [TestFixture]
    public sealed class CollectionCountConverterTests
    {
        private static readonly string[] _three = { "a", "b", "c" };

        [Test]
        public void Count_CountsTheItems() =>
            Assert.AreEqual(3, new CollectionCountConverter<string>().Convert(_three));

        [Test]
        public void Count_NullIsZero() =>
            Assert.AreEqual(0, new CollectionCountConverter<string>().Convert(null));

        // An iterator carries no count of its own, so the counted-collection member cannot take it at
        // all: it arrives through the sequence member and is walked.
        [Test]
        public void Count_WalkedSequence_CountsTheItems() =>
            Assert.AreEqual(3, SequenceCountAsInt().Convert(Walk("a", "b", "c")));

        [Test]
        public void Count_WalkedSequence_EmptyIsZero() =>
            Assert.AreEqual(0, SequenceCountAsInt().Convert(Walk<string>()));

        [Test]
        public void Count_WalkedSequence_NullIsZero() =>
            Assert.AreEqual(0, SequenceCountAsInt().Convert(null));

        // One number, and the binding decides which numeric member carries it.
        [Test]
        public void Count_WidensToTheBoundNumericType()
        {
            Assert.AreEqual(3L, CollectionCountAsLong().Convert(_three));
            Assert.AreEqual(3f, CollectionCountAsFloat().Convert(_three), 1e-5f);
            Assert.AreEqual(3d, CollectionCountAsDouble().Convert(_three), 1e-12);
        }

        [Test]
        public void Count_WalkedSequence_WidensToTheBoundNumericType()
        {
            Assert.AreEqual(3L, SequenceCountAsLong().Convert(Walk("a", "b", "c")));
            Assert.AreEqual(3f, SequenceCountAsFloat().Convert(Walk("a", "b", "c")), 1e-5f);
            Assert.AreEqual(3d, SequenceCountAsDouble().Convert(Walk("a", "b", "c")), 1e-12);
        }

        [Test]
        public void Count_List_ArrivingAsASequence_IsStillCounted() =>
            Assert.AreEqual(3, SequenceCountAsInt().Convert(new List<string> { "a", "b", "c" }));

        // A sequence that knows its own size is asked for it rather than walked. Both fakes throw on
        // being enumerated, so a rewrite that lost the fast path fails here instead of merely walking
        // a long sequence on every push.
        [Test]
        public void Count_SequenceWithACount_TakesItRatherThanWalking()
        {
            Assert.AreEqual(3, SequenceCountAsInt().Convert(new CountedOnlyCollection(3)));
            Assert.AreEqual(3, SequenceCountAsInt().Convert(new MutableCountedOnlyCollection(3)));
        }

        // The count and emptiness members over a sequence are explicit, so every call above goes
        // through the interface the binding would use rather than the public method.
        private static IConverter<IEnumerable<string>, int> SequenceCountAsInt() =>
            new CollectionCountConverter<string>();

        private static IConverter<IEnumerable<string>, long> SequenceCountAsLong() =>
            new CollectionCountConverter<string>();

        private static IConverter<IEnumerable<string>, float> SequenceCountAsFloat() =>
            new CollectionCountConverter<string>();

        private static IConverter<IEnumerable<string>, double> SequenceCountAsDouble() =>
            new CollectionCountConverter<string>();

        private static IConverter<IReadOnlyCollection<string>, long> CollectionCountAsLong() =>
            new CollectionCountConverter<string>();

        private static IConverter<IReadOnlyCollection<string>, float> CollectionCountAsFloat() =>
            new CollectionCountConverter<string>();

        private static IConverter<IReadOnlyCollection<string>, double> CollectionCountAsDouble() =>
            new CollectionCountConverter<string>();

        // An iterator, so the result is an IEnumerable and nothing more — no count is available to
        // take and the walking branch is the one under test.
        private static IEnumerable<T> Walk<T>(params T[] items)
        {
            foreach (var item in items)
                yield return item;
        }
    }
}
