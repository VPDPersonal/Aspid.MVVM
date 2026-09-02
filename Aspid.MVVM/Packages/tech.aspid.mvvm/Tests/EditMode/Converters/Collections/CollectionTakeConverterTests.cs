#nullable enable
using System;
using NUnit.Framework;
using System.Reflection;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="CollectionTakeConverter{T}"/> — the indexed and walked paths, the
    /// from-end variant, the count guards, and the reused output buffer.
    /// </summary>
    /// <remarks>
    /// The list this converter hands out is shared and asserted by reference.
    /// </remarks>
    [TestFixture]
    public sealed class CollectionTakeConverterTests
    {
        private static readonly int[] _five = { 1, 2, 3, 4, 5 };

        [Test]
        public void Take_List_KeepsTheItemsOffTheStart() =>
            CollectionAssert.AreEqual(new[] { 1, 2 }, new CollectionTakeConverter<int>(2).Convert(_five));

        [Test]
        public void Take_WalkedSequence_KeepsTheItemsOffTheStart() =>
            CollectionAssert.AreEqual(new[] { 1, 2 }, new CollectionTakeConverter<int>(2).Convert(Streamed(1, 2, 3, 4, 5)));

        // Off the end, but still in the sequence's own order — not reversed, which is the shape a
        // "last five lines of a log" view would be wrong in without anyone noticing for a while.
        [Test]
        public void Take_List_FromEnd_KeepsTheTailInItsOriginalOrder() =>
            CollectionAssert.AreEqual(new[] { 4, 5 }, new CollectionTakeConverter<int>(2, fromEnd: true).Convert(_five));

        [Test]
        public void Take_WalkedSequence_FromEnd_KeepsTheTailInItsOriginalOrder() =>
            CollectionAssert.AreEqual(
                new[] { 4, 5 },
                new CollectionTakeConverter<int>(2, fromEnd: true).Convert(Streamed(1, 2, 3, 4, 5)));

        [TestCase(false)]
        [TestCase(true)]
        public void Take_CountAboveTheLength_KeepsEverything(bool fromEnd)
        {
            CollectionAssert.AreEqual(_five, new CollectionTakeConverter<int>(50, fromEnd).Convert(_five));
            CollectionAssert.AreEqual(_five, new CollectionTakeConverter<int>(50, fromEnd).Convert(Streamed(1, 2, 3, 4, 5)));
        }

        // Zero is the value a count field is left at while it is being typed into, and a negative one
        // is what an old asset carries. Neither may index past the front, and neither may let the
        // whole sequence through.
        [TestCase(0, false)]
        [TestCase(0, true)]
        [TestCase(-1, false)]
        [TestCase(-1, true)]
        public void Take_CountOfZeroOrLess_KeepsNothing(int count, bool fromEnd)
        {
            CollectionAssert.IsEmpty(Taking(count, fromEnd).Convert(_five));
            CollectionAssert.IsEmpty(Taking(count, fromEnd).Convert(Streamed(1, 2, 3, 4, 5)));
        }

        // Keeping none of the items is a legal setting; keeping a negative number of them is not.
        [Test]
        public void Take_NegativeCount_IsRejectedByTheConstructor() =>
            Assert.Throws<ArgumentOutOfRangeException>(() => new CollectionTakeConverter<int>(-1));

        [Test]
        public void Take_CountOfZero_IsAcceptedAndKeepsNothing() =>
            CollectionAssert.IsEmpty(new CollectionTakeConverter<int>(0).Convert(_five));

        [TestCase(false)]
        [TestCase(true)]
        public void Take_Null_KeepsNothing(bool fromEnd) =>
            CollectionAssert.IsEmpty(new CollectionTakeConverter<int>(2, fromEnd).Convert(null));

        [TestCase(false)]
        [TestCase(true)]
        public void Take_EmptySequence_KeepsNothing(bool fromEnd)
        {
            CollectionAssert.IsEmpty(new CollectionTakeConverter<int>(2, fromEnd).Convert(Array.Empty<int>()));
            CollectionAssert.IsEmpty(new CollectionTakeConverter<int>(2, fromEnd).Convert(Streamed<int>()));
        }

        [Test]
        public void Take_DefaultConstructed_KeepsThreeOffTheStart() =>
            CollectionAssert.AreEqual(new[] { 1, 2, 3 }, new CollectionTakeConverter<int>().Convert(_five));

        // Off the start, the walk stops as soon as it has enough — the whole reason a "top three" over
        // a long feed is affordable. A converter that buffered everything first would read 100 here.
        [Test]
        public void Take_WalkedSequence_StopsPullingOnceItHasEnough()
        {
            var probe = new SequencePullProbe();

            CollectionAssert.AreEqual(new[] { 1, 2 }, new CollectionTakeConverter<int>(2).Convert(Counted(100, probe)));
            Assert.AreEqual(2, probe.Pulls);
        }

        // Off the end it cannot: where the last two items begin is unknown until the sequence ends, so
        // the tail case pays for the whole walk. Asserted because it is the asymmetry that decides
        // whether this converter belongs on a long feed at all.
        [Test]
        public void Take_WalkedSequence_FromEnd_PullsEveryItem()
        {
            var probe = new SequencePullProbe();

            CollectionAssert.AreEqual(
                new[] { 99, 100 },
                new CollectionTakeConverter<int>(2, fromEnd: true).Convert(Counted(100, probe)));

            Assert.AreEqual(100, probe.Pulls);
        }

        // The contract that makes this converter allocation-free on a binder that pushes every frame.
        [Test]
        public void Take_ReturnsTheSameListInstanceOnEveryCall()
        {
            var converter = new CollectionTakeConverter<int>(2);

            Assert.AreSame(converter.Convert(new[] { 1, 2, 3 }), converter.Convert(new[] { 7, 8, 9 }));
        }

        // The early return for a null or a non-positive count leaves through a different line, and it
        // has to hand back the same list rather than a fresh empty one.
        [Test]
        public void Take_ReturnsTheSameListInstanceAcrossTheNullAndEmptyPaths()
        {
            var converter = new CollectionTakeConverter<int>(2);

            Assert.AreSame(converter.Convert(null), converter.Convert(_five));
            Assert.AreSame(converter.Convert(_five), converter.Convert(Streamed(1, 2, 3)));
        }

        // The price of that reuse, stated as a test so nobody has to discover it: a result held past
        // the push it arrived on is a view of whatever came last, not a snapshot.
        [Test]
        public void Take_ResultHeldAcrossCalls_ShowsTheLatestConversion()
        {
            var converter = new CollectionTakeConverter<int>(2);
            var held = converter.Convert(new[] { 1, 2, 3 });

            converter.Convert(new[] { 7, 8, 9 });

            CollectionAssert.AreEqual(new[] { 7, 8 }, held);
        }

        // ...and a null push does not leave the old items standing: the buffer is cleared first.
        [Test]
        public void Take_ResultHeldAcrossANullConversion_IsEmptied()
        {
            var converter = new CollectionTakeConverter<int>(2);
            var held = converter.Convert(new[] { 1, 2, 3 });

            converter.Convert(null);

            CollectionAssert.IsEmpty(held);
        }

        // The buffer is per instance, not static. Two binders sharing one would overwrite each other
        // and the symptom would depend on which one pushed last.
        [Test]
        public void Take_SeparateInstances_DoNotShareABuffer()
        {
            var first = new CollectionTakeConverter<int>(2);
            var second = new CollectionTakeConverter<int>(2);

            var firstResult = first.Convert(new[] { 1, 2, 3 });
            var secondResult = second.Convert(new[] { 7, 8, 9 });

            Assert.AreNotSame(firstResult, secondResult);
            CollectionAssert.AreEqual(new[] { 1, 2 }, firstResult);
        }

        // A count the constructor refuses can still arrive from a serialized asset, so the fields are
        // written the way deserialization writes them.
        private static CollectionTakeConverter<int> Taking(int count, bool fromEnd)
        {
            var converter = new CollectionTakeConverter<int>();

            Field("_count").SetValue(converter, count);
            Field("_fromEnd").SetValue(converter, fromEnd);

            return converter;

            static FieldInfo Field(string name) =>
                typeof(CollectionTakeConverter<int>)
                    .GetField(name, BindingFlags.Instance | BindingFlags.NonPublic)!;
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
