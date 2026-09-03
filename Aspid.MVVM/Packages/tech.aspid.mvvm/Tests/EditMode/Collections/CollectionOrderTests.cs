#nullable enable
using System;
using System.Linq;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for the composite and leaf <see cref="ICollectionOrder{T}"/> implementations.
    /// </summary>
    [TestFixture]
    public sealed class CollectionOrderTests
    {
        private static readonly ICollectionOrder<(int Group, int Value)> ByGroup =
            new ComparisonCollectionOrder<(int Group, int Value)>((x, y) => x.Group.CompareTo(y.Group));

        private static readonly ICollectionOrder<(int Group, int Value)> ByValue =
            new ComparisonCollectionOrder<(int Group, int Value)>((x, y) => x.Value.CompareTo(y.Value));

        [Test]
        public void Comparison_DelegatesToTheWrappedComparison() =>
            Assert.Less(ByValue.Compare((0, 1), (0, 2)), 0);

        [Test]
        public void Comparison_NullInTheConstructor_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => _ = new ComparisonCollectionOrder<int>((IComparer<int>)null!));
            Assert.Throws<ArgumentNullException>(() => _ = new ComparisonCollectionOrder<int>((Comparison<int>)null!));
        }

        [Test]
        public void Sequence_BreaksTiesWithTheNextOrder()
        {
            var order = new SequenceCollectionOrder<(int Group, int Value)>(ByGroup, ByValue);
            var source = new[] { (1, 2), (0, 5), (1, 1), (0, 3) };

            var sorted = source.OrderBy(item => item, order).ToArray();

            CollectionAssert.AreEqual(new[] { (0, 3), (0, 5), (1, 1), (1, 2) }, sorted);
        }

        [Test]
        public void Inverse_FlipsTheOrder()
        {
            var order = new InverseCollectionOrder<(int Group, int Value)>(ByValue);

            Assert.Greater(order.Compare((0, 1), (0, 2)), 0);
            Assert.AreEqual(0, order.Compare((0, 1), (0, 1)));
        }

        [Test]
        public void Inverse_NullInTheConstructor_Throws() =>
            Assert.Throws<ArgumentNullException>(() => _ = new InverseCollectionOrder<int>(null!));

        [Test]
        public void Sequence_SkipsEmptySlots()
        {
            var order = new SequenceCollectionOrder<int>(null, Ascending());

            Assert.Less(order.Compare(1, 2), 0);
        }

        [Test]
        public void Sequence_NoOrders_KeepsEverythingEqual() =>
            Assert.AreEqual(0, new SequenceCollectionOrder<int>().Compare(1, 2));

        [Test]
        public void Sequence_NullArray_KeepsEverythingEqual() =>
            Assert.AreEqual(0, new SequenceCollectionOrder<int>(null).Compare(1, 2));

        private static ICollectionOrder<int> Ascending() =>
            new ComparisonCollectionOrder<int>(Comparer<int>.Default);
    }
}
